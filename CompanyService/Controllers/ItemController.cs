using CompanyService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace CompanyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<UserModel> _userRepository;

        public IRepository<CatagoryModel> _catagoryRepository { get; }
        public IYattHttpClient<CompanyContract> _httpClient { get; }

        public ItemController(IRepository<Company> companyRepository, IRepository<UserModel> userRepository,
             IRepository<CatagoryModel> catagoryRepository, IYattHttpClient<CompanyContract> httpClient)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _catagoryRepository = catagoryRepository;
            _httpClient = httpClient;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _companyRepository.GetAsync(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _companyRepository.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompanyDto item)
        {
            var user = await _userRepository.GetAsync(a => a.Id == item.Id);

            if (user == null) return NotFound($"User with id : {item.Id} could not found");

            var catagory = await _catagoryRepository.GetAsync(a => a.Id == item.CatagoryId);

            if (catagory == null) return NotFound($"Catagory with id : {item.Id} could not found");

            var current = DateTime.UtcNow;
            var company = item.GetItem();

            company.CreatedDate = current;
            company.ModifiedDate = current;
            try
            {
                await _companyRepository.CreateAsync(company);
                await _httpClient.PostAsync("http://host.docker.internal:19008/api/contracts/company",
                    new CompanyContract(company.Id,company.CompanyName,company.UseTrial,company.Status,company.ModifiedDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = company.Id }, company);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, CompanyDto item)
        {
            var company = await _companyRepository.GetAsync(id);

            if (company == null) return NotFound($"Company with Id : {id} could not found");
            
            var catagory = await _catagoryRepository.GetAsync(a => a.Id == item.CatagoryId);
            
            if (catagory == null) return BadRequest($"Invalid catagory with id : {item.CatagoryId}");

            company.CompanyName= item.CompanyName;
            company.TinNumber= item.TinNumber;
            company.CompanyPhone= item.CompanyPhone;
            company.ContactName= item.ContactName;
            company.ContactPhone= item.ContactPhone;
            company.Address= item.Address;
            company.Description= item.Description;
            company.WebUrl= item.WebUrl;

            company.ModifiedDate = DateTime.UtcNow;

            await _companyRepository.UpdateAsync(company);
            await _httpClient.PostAsync("http://host.docker.internal:19008/api/contracts/company",
                    new CompanyContract(company.Id,company.CompanyName,company.UseTrial,company.Status,company.ModifiedDate));

            return Ok(company);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var company = await _companyRepository.GetAsync(id);

            if (company == null) return NotFound();

            await _companyRepository.DeleteAsync(id);
            await _httpClient.DeleteAsync("http://host.docker.internal:19008/api/contracts/company", id.ToString());
            return Ok(company);
        }
    }
}
