using MembershipService.Models;
using Microsoft.AspNetCore.Mvc;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace MembershipService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Membership> _repository;
        private readonly IYattHttpClient<MembershipContract> _httpClient;
        public ItemController(IRepository<Membership> repository, IYattHttpClient<MembershipContract> httpClient)
        {
            _repository = repository;
            this._httpClient = httpClient;
        }
       
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var membership = await _repository.GetAsync(id);

            if (membership == null) return NotFound();

            return Ok(membership.AsDto());
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok((await _repository.GetAllAsync()).Select(a=>a.AsDto()));
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MembershipDto item)
        {
            var current = DateTime.UtcNow;
            var membership = new Membership()
            {
                Name = item.Name,
                UsageInMonth = item.UsageInMonth,
                NoOfJobPosted = item.NoOfJobPosted,
                Price = item.Price,
                CreatedDate = current,
                ModifiedDate = current
            };
            
            try
            {
                await _repository.CreateAsync(membership);
                await _httpClient.PostAsync("http://host.docker.internal:19008/api/contracts/membership", 
                    new MembershipContract(membership.Id, membership.Name, membership.UsageInMonth, membership.NoOfJobPosted, membership.Price, membership.ModifiedDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = membership.Id }, membership.AsDto());
        }
      
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, MembershipDto item)
        {
            var membership = await _repository.GetAsync(id);

            if (membership == null) return NotFound();
            membership.Name= item.Name;
            membership.NoOfJobPosted= item.NoOfJobPosted;
            membership.Price= item.Price;
            membership.UsageInMonth= item.UsageInMonth;
            membership.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _repository.UpdateAsync(membership);
                await _httpClient.PostAsync("http://host.docker.internal:19008/api/contracts/membership",
                    new MembershipContract(membership.Id, membership.Name, membership.UsageInMonth, membership.NoOfJobPosted, membership.Price, membership.ModifiedDate));
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(membership.AsDto());
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _repository.DeleteAsync(id);

                await _httpClient.DeleteAsync("http://host.docker.internal:19008/api/contracts/membership", id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item.AsDto());
        }
    }
}
