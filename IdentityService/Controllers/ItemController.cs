using IdentityService.Handlers;
using IdentityService.Models;
using Microsoft.AspNetCore.Mvc;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Constants;
using YattCommon.Contracts;
using YattCommon.Dtos.Account;
using YattCommon.Enums;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly TokenManager _tokenManager;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserToken> _userTokenRepository;

        private readonly IYattHttpClient<IdentityContract> _httpClient;

        public ItemController(TokenManager tokenManager, IRepository<User> repository,
            IRepository<UserToken> tokenRepository, IYattHttpClient<IdentityContract> yattHttpClient)
        {
            _tokenManager = tokenManager;
            this._userRepository = repository;
            this._userTokenRepository = tokenRepository;
            this._httpClient = yattHttpClient;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null) return BadRequest();

            string role = registerDto.Role.ToLower();
            string userRole = string.Empty;

            if(role == RoleConstant.CANDIDATE.ToLower()) userRole = RoleConstant.CANDIDATE;
            if(role == RoleConstant.EMPLOYEER.ToLower()) userRole = RoleConstant.EMPLOYEER;

            if (string.IsNullOrEmpty(userRole))
                return BadRequest($"{userRole} : This role is not found");

            var user = await _userRepository.GetAsync(a => a.Email == registerDto.Email);

            if(user != null) return BadRequest($"The email address {registerDto.Email} is already in use");
           
            byte[] hash, salt;
            PasswordHasher.GeneratePasswordHasing(registerDto.Password, out salt, out hash);
            var current = DateTime.UtcNow;
            
           
            user = new User
            {
                Email = registerDto.Email,
                EmailConfirmed = false,
                CreatedDate = current,
                ModifiedDate = current,
                LastLogin = current,
                LockCount = 0,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = userRole
            };

            try
            {
                await _userRepository.CreateAsync(user);
                
                Random random = new Random();
                var token = string.Format("{0:000000}", random.Next(0, 999999));

                var userToken = new UserToken
                {
                    UserId=user.Id,
                    Token = token,
                    CreatedDate = current,
                    ExpiredTime = DateTime.UtcNow.AddHours(24),
                    TokenType = TokenType.EmailConfirmation
                };

                await _userTokenRepository.CreateAsync(userToken);
                if (userRole.ToLower() == RoleConstant.CANDIDATE.ToLower())
                {
                    await _httpClient.PostAsync("http://host.docker.internal:19006/api/contracts/user", 
                        new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate));
                }
                else if(userRole.ToLower() == RoleConstant.EMPLOYEER.ToLower())
                    await _httpClient.PostAsync("http://host.docker.internal:19007/api/contracts/user",
                        new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate));


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
            return CreatedAtAction(nameof(GetByIdAsync),new { id = user.Id },user.AsDto());
        }

        [HttpPost("conferm_email")]
        public async Task<IActionResult> ConfirmEmailAsync(Guid id,string token)
        {
            var userToken=await _userTokenRepository.GetAsync(a=>a.UserId==id && a.TokenType==TokenType.EmailConfirmation);

            if (userToken == null) return NotFound("Token is not found");

            if (!userToken.Token!.Equals(token)) return BadRequest("Invalid token");

            if (userToken.ExpiredTime<DateTime.UtcNow) return BadRequest("This Token is expired");

            var user = await _userRepository.GetAsync(id);
            if (user == null) return NotFound($"User with id :{id} is not found.");
           
            if (user.EmailConfirmed) return Ok($"User is already confirm email");
            
            try
            {
                user.EmailConfirmed= true;
                user.ModifiedDate= DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

                if (user.Role.ToLower() == RoleConstant.CANDIDATE.ToLower())
                    await _httpClient.PostAsync("http://host.docker.internal:19006/api/contracts/user",
                        new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate));
                else if (user.Role.ToLower() == RoleConstant.EMPLOYEER.ToLower())
                    await _httpClient.PostAsync("http://host.docker.internal:19007/api/contracts/user",
                        new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Email confermation is success");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            if (loginDto == null) return BadRequest();

           var user=await _userRepository.GetAsync(a=>a.Email==loginDto.Email);

            if (user == null) return NotFound("User is not found");
            
            if (!user.EmailConfirmed) return Unauthorized("Email is not confirmed");

            var valid=await PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordSalt,user.PasswordHash);
            var current = DateTime.UtcNow;

            if (!valid)
            {
                user.LockCount += 1;
                user.ModifiedDate = current;
                await _userRepository.UpdateAsync(user);

                return BadRequest("Email or password is not valid");
            }
          
            user.LastLogin=current;

            var token = await _tokenManager.GenerateToken(user);

            try
            {
                await _userRepository.UpdateAsync(user);
                if (user.Role.ToLower() == RoleConstant.CANDIDATE.ToLower())
                    await _httpClient.PostAsync("http://host.docker.internal:19006/api/contracts/user", 
                        new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate));
                else if (user.Role.ToLower() == RoleConstant.EMPLOYEER.ToLower())
                    await _httpClient.PostAsync("http://host.docker.internal:19007/api/contracts/user",
                        new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new AuthDto { Token=token,TokenExpireTime=DateTime.UtcNow.AddMinutes(30),RefreshToken="RefreshToken"});
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok((await _userRepository.GetAllAsync()).Select(a=>a.AsDto()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user=await _userRepository.GetAsync(id);

            if (user == null) return NotFound();

            return Ok(user.AsDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null) return NotFound();
            try
            {
                await _userRepository.DeleteAsync(id);
                if (user.Role.ToLower() == RoleConstant.CANDIDATE.ToLower())
                    await _httpClient.DeleteAsync("http://host.docker.internal:19006/api/contracts/user", id.ToString());
                else if (user.Role.ToLower() == RoleConstant.EMPLOYEER.ToLower())
                    await _httpClient.DeleteAsync("http://host.docker.internal:19007/api/contracts/user",id.ToString());

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(user.AsDto());
        }
    }
}
