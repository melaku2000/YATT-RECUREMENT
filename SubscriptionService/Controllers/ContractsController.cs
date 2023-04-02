using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Models;
using YattCommon;
using YattCommon.Contracts;

namespace SubscriptionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IRepository<MembershipModel> _memberRepository;
        public IRepository<CompanyModel> _companyRepository { get; }
        public ContractsController(IRepository<MembershipModel> memberRepository, IRepository<CompanyModel> companyRepository)
        {
            _memberRepository = memberRepository;
            _companyRepository = companyRepository;
        }


        [HttpPost("membership")]
        public async Task<IActionResult> CreateMemberAsync(MembershipContract item)
        {
            var member = await _memberRepository.GetAsync(item.Id);

            try
            {
                if (member == null)
                {
                    member = new MembershipModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        UsageInMonth = item.UsageInMonth,
                        NoOfJobPosted = item.NoOfJobPosted,
                        Price = item.Price, 
                        ModifiedDate = item.ModifiedDate
                    };

                    await _memberRepository.CreateAsync(member);
                }
                else
                {
                    member.Name = item.Name;
                    member.UsageInMonth = item.UsageInMonth;
                    member.NoOfJobPosted = item.NoOfJobPosted;
                    member.Price = item.Price;
                    member.ModifiedDate = item.ModifiedDate;

                    await _memberRepository.UpdateAsync(member);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(member);
        }
      
        [HttpDelete("membership/{id:guid}")]
        public async Task<IActionResult> DeleteMemberAsync(Guid id)
        {
            var item = await _memberRepository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _memberRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item);
        }

        [HttpPost("company")]
        public async Task<IActionResult> CreateCompanyAsync(CompanyContract item)
        {
            var company = await _companyRepository.GetAsync(item.Id);

            try
            {
                if (company == null)
                {
                    company = new CompanyModel()
                    {
                        Id = item.Id,
                        CompanyName = item.CompanyName,
                        ModifiedDate = item.ModifiedDate,
                        UseTrial = item.UseTrial,
                        Status = item.Status,
                    };

                    await _companyRepository.CreateAsync(company);
                }
                else
                {
                    company.UseTrial = item.UseTrial;
                    company.CompanyName = item.CompanyName;
                    company.Status = item.Status;
                    company.ModifiedDate = item.ModifiedDate;

                    await _companyRepository.UpdateAsync(company);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(company);
        }

        [HttpDelete("company/{id:guid}")]
        public async Task<IActionResult> DeleteCompanyAsync(Guid id)
        {
            var company = await _companyRepository.GetAsync(id);

            if (company == null) return NotFound();
            try
            {
                await _companyRepository.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(company);
        }
    }
}