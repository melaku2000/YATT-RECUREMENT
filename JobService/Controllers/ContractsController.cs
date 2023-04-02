using JobService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Contracts;

namespace JobService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        public IRepository<SubscribeModel> _subscribeRepository { get; }
        public ContractsController(IRepository<SubscribeModel> subscribeRepository)
        {
            _subscribeRepository = subscribeRepository;
        }
        [HttpPost("subscribe")]
        public async Task<IActionResult> CreateSubscribeAsync(SubscriptionContract item)
        {

            var subscribe = await _subscribeRepository.GetAsync(item.Id);

            try
            {
                if (subscribe == null)
                {
                    subscribe = new SubscribeModel()
                    {
                        Id = item.Id, CompanyId=item.CompanyId, CompanyName=item.CompanyName, UsageInMonth=item.UsageInMonth,
                         NoOfJobPosted=item.NoOfJobPosted,  Status=item.Status,
                        ModifiedDate = item.ModifiedDate
                    };

                    await _subscribeRepository.CreateAsync(subscribe);
                }
                else
                {
                    subscribe.UsageInMonth = item.UsageInMonth;
                    subscribe.NoOfJobPosted = item.NoOfJobPosted;
                    subscribe.Status = item.Status;
                    subscribe.ModifiedDate = item.ModifiedDate;

                    await _subscribeRepository.UpdateAsync(subscribe);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(subscribe);
        }

        [HttpDelete("subscribe/{id:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {

            var subscribe = await _subscribeRepository.GetAsync(id);

            if (subscribe == null) return NotFound($"The item with id : {id} could not found");

            try
            {
                await _subscribeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(subscribe);
        }
    }
}
