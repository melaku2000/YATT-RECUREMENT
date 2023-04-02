using CatagoryService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace CatagoryService.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Catagory> _repository;
        private readonly IYattHttpClient<CatagoryContract> _httpClient;
        public ItemController(IRepository<Catagory> repository, IYattHttpClient<CatagoryContract> httpClient)
        {
            _repository = repository;
            _httpClient = httpClient;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var catagory = await _repository.GetAsync(id);

            if (catagory == null) return NotFound();

            return Ok(catagory.AsDto());
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok((await _repository.GetAllAsync()).Select(a=>a.AsDto()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CatagoryDto item)
        {
            var current = DateTime.UtcNow;
            var catagory = new Catagory
            {
                Title = item.Title,
                ModifiedDate = current,
                CreatedDate = current
            };
          
            try
            {
                await _repository.CreateAsync(catagory);
                await _httpClient.PostAsync("http://host.docker.internal:19007/api/contracts/catagory",
                    new CatagoryContract(catagory.Id, catagory.Title, catagory.ModifiedDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = catagory.Id }, catagory.AsDto());
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, CatagoryDto item)
        {
            var catagory = await _repository.GetAsync(id);

            if (catagory == null) return NotFound();
            try
            {
                catagory.Title= item.Title;
                catagory.ModifiedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(catagory);
                await _httpClient.PostAsync("http://host.docker.internal:19007/api/contracts/catagory",
                    new CatagoryContract(catagory.Id, catagory.Title, catagory.ModifiedDate));
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

                await _httpClient.DeleteAsync("http://host.docker.internal:19007/api/contracts/catagory", id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item.AsDto());
        }
    }
}
