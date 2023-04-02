using LanguageService.Models;
using Microsoft.AspNetCore.Mvc;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace LanguageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Language> _repository;
        private readonly IYattHttpClient<LanguageContract> _httpClient;

        public ItemController(IRepository<Language> repository, IYattHttpClient<LanguageContract> httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }
      
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var language = await _repository.GetAsync(id);

            if (language == null) return NotFound();

            return Ok(language.AsDto());
        }
      
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok((await _repository.GetAllAsync()).Select(a=>a.AsDto()));
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateAsync(LanguageDto item)
        {
            var current = DateTime.UtcNow;
            var language = new Language()
            {
                Name = item.Name,
                CreatedDate = current,
                ModifiedDate=current
            };
            
            try
            {
                await _repository.CreateAsync(language);
                await _httpClient.PostAsync("http://host.docker.internal:19006/api/contracts/language", 
                    new LanguageContract(language.Id,language.Name,language.ModifiedDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = language.Id }, language.AsDto());
        }
      
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, LanguageDto item)
        {
            var language = await _repository.GetAsync(id);

            if (language == null) return NotFound();
           
            language.Name=item.Name;
            language.ModifiedDate=DateTime.UtcNow;

            try
            {
                await _repository.UpdateAsync(language);
                await _httpClient.PostAsync("http://host.docker.internal:19006/api/contracts/language",
                    new LanguageContract(language.Id,language.Name,language.ModifiedDate));
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item);
        }
       
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _repository.DeleteAsync(id);
                await _httpClient.DeleteAsync("http://host.docker.internal:19006/api/contracts/language", id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item.AsDto());
        }
    }
}