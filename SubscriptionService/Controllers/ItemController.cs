using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Models;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;
using YattCommon.Enums;

namespace SubscriptionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IRepository<CompanyModel> _companyRepository;
        private readonly IRepository<MembershipModel> _memberRepository;

        private readonly IYattHttpClient<SubscriptionContract> _httpClient;

        public ItemController(IRepository<Subscription> subscriptionRepository, IRepository<CompanyModel> companyRepository, 
            IRepository<MembershipModel> memberRepository, IYattHttpClient<SubscriptionContract> httpClient)
        {
            _subscriptionRepository = subscriptionRepository;
            this._companyRepository = companyRepository;
            this._memberRepository = memberRepository;

            _httpClient = httpClient;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _subscriptionRepository.GetAsync(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
        [HttpGet("listById/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _subscriptionRepository.GetAllAsync(a=>a.CompanyId==id));
        }
        [HttpGet("listall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var companies=await _companyRepository.GetAllAsync();
            var subscribers = (await _subscriptionRepository.GetAllAsync())
                .Select(a =>
                {
                    var company = companies.FirstOrDefault(x => x.Id == a.CompanyId);
                    return a.AsDto(company.CompanyName);
                });
            return Ok(subscribers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SubscriptionDto item)
        {
            var company = await _companyRepository.GetAsync(a => a.Id == item.CompanyId);
            if (company == null) return NotFound($"Company with id : {item.CompanyId} could not found");

            var member = await _memberRepository.GetAsync(a => a.Id == item.MembershipId);
            if (member == null) return NotFound($"Member with id : {item.MembershipId} could not found");

            var subscription=item.GetItem();
            subscription.UsageInMonth=member.UsageInMonth;
            subscription.NoOfJobPosted=member.NoOfJobPosted;
            subscription.Price = member.Price;
            subscription.Status = SubscriptionStatus.Pending;
            subscription.CreatedDate=subscription.ModifiedDate=DateTime.UtcNow;
            
            try
            {
                await _subscriptionRepository.CreateAsync(subscription);
                await _httpClient.PostAsync("http://host.docker.internal:19009/api/contracts/subscribe",
                    new SubscriptionContract(subscription.Id, subscription.CompanyId,company.CompanyName,
                    subscription.UsageInMonth,subscription.NoOfJobPosted,subscription.Status, subscription.ModifiedDate));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = subscription.Id }, subscription.AsDto(""));
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, SubscriptionDto item)
        {
            var subscription = await _subscriptionRepository.GetAsync(id);

            if (subscription == null) return NotFound();
       
            var member = await _memberRepository.GetAsync(a => a.Id == item.MembershipId);
            if (member == null) return NotFound($"Member with id : {item.MembershipId} could not found");

            var company = await _companyRepository.GetAsync(a => a.Id == item.CompanyId);
            if (company == null) return NotFound($"Company with id : {item.CompanyId} could not found");

            subscription.UsageInMonth=member.UsageInMonth;
            subscription.NoOfJobPosted=member.NoOfJobPosted;
            subscription.Price=member.Price;
            subscription.Status = item.Status;
            subscription.ModifiedDate = DateTime.UtcNow;

            try
            {

                await _subscriptionRepository.UpdateAsync(subscription);
                await _httpClient.PostAsync("http://host.docker.internal:19009/api/contracts/subscribe",
                  new SubscriptionContract(subscription.Id, subscription.CompanyId, company.CompanyName,
                  subscription.UsageInMonth, subscription.NoOfJobPosted, subscription.Status, subscription.ModifiedDate));

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(subscription.AsDto(company.CompanyName));
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _subscriptionRepository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _subscriptionRepository.DeleteAsync(id);

                await _httpClient.DeleteAsync("http://host.docker.internal:19009/api/contracts/subscribe", id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item);
        }
    }
}