using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon.Contracts;
using YattCommon;
using CompanyService.Models;

namespace CompanyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IRepository<UserModel> _userRepository;
        public IRepository<CatagoryModel> _catagoryRepository { get; }
        public ContractsController(IRepository<UserModel> userRepository, IRepository<CatagoryModel> catagoryRepository)
        {
            _userRepository = userRepository;
            _catagoryRepository = catagoryRepository;
        }


        [HttpPost("user")]
        public async Task<IActionResult> CreateUserAsync(IdentityContract item)
        {

            var user = await _userRepository.GetAsync(item.Id);

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
        [HttpPost("catagory")]
        public async Task<IActionResult> CreateCatagoryAsync(CatagoryContract item)
        {

            var catagory = await _catagoryRepository.GetAsync(item.Id);

            try
            {
                if (catagory == null)
                {
                    catagory = new CatagoryModel()
                    {
                        Id = item.Id,
                        Title = item.Title, 
                        ModifiedDate = item.ModifiedDate
                    };

                    await _catagoryRepository.CreateAsync(catagory);
                }
                else
                {
                    catagory.Title = item.Title;
                    catagory.ModifiedDate = item.ModifiedDate;

                    await _catagoryRepository.UpdateAsync(catagory);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(catagory);
        }

        [HttpDelete("catagory/{id:guid}")]
        public async Task<IActionResult> DeleteCatagoryAsync(Guid id)
        {

            var catagory = await _catagoryRepository.GetAsync(id);

            if (catagory == null) return NotFound($"The item with id : {id} could not found");

            try
            {
                await _catagoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(catagory);
        }
    }
}
