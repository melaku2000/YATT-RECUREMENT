using CandidateService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace CandidateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IRepository<UserModel> _userRepository;
        private readonly IRepository<LanguageModel> _languageRepository;
        public ContractsController(IRepository<UserModel> userRepository, IRepository<LanguageModel> languageRepository)
        {
            _userRepository = userRepository;
            _languageRepository = languageRepository;
        }
        [HttpPost("user")]
        public async Task<IActionResult> CreateUserAsync(IdentityContract item)
        {
            
            var user=await _userRepository.GetAsync(item.Id);
           
            try
            {
                if (user == null)
                {
                    user = new UserModel()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        EmailConfirmed = item.EmailConfirmed,
                        ModifiedDate = item.ModifiedDate
                    };
                   
                    await _userRepository.CreateAsync(user);
                }
                else
                {
                    user.Email = item.Email;
                    user.EmailConfirmed = item.EmailConfirmed;
                    user.ModifiedDate = item.ModifiedDate;

                    await _userRepository.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(user);
        }

        [HttpDelete("user/{id:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {

            var user = await _userRepository.GetAsync(id);

            if (user == null) return NotFound($"The item with id : {id} could not found");

            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(user);
        }

        [HttpPost("language")]
        public async Task<IActionResult> CreateAsync(LanguageContract item)
        {

            var language = await _languageRepository.GetAsync(item.Id);

            try
            {
                if (language == null)
                {
                    language = new LanguageModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ModifiedDate = item.ModifiedDate
                    };

                    await _languageRepository.CreateAsync(language);
                }
                else
                {
                    language.Name = item.Name;
                    language.ModifiedDate = item.ModifiedDate;

                    await _languageRepository.UpdateAsync(language);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(language);
        }

        [HttpDelete("language/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {

            var language = await _languageRepository.GetAsync(id);

            if (language == null) return NotFound($"The item with id : {id} could not found");

            try
            {
                await _languageRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(language);
        }
    }
}
