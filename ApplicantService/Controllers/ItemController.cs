using ApplicantService.Models;
using Microsoft.AspNetCore.Mvc;
using YattCommon;

namespace ApplicantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Applicant> _applicantRepository;
        private readonly IRepository<CandidateModel> _candidateRepository;
        public ItemController(IRepository<Applicant> repository, IRepository<CandidateModel> candidateRepository)
        {
            _applicantRepository = repository;
            _candidateRepository = candidateRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var applicant = await _applicantRepository.GetAsync(id);

            if (applicant == null) return NotFound($"The item with id : {id} could not found");
             var candidate= await _candidateRepository.GetAsync(applicant.CandidateId);
            return Ok(applicant.AsDto(candidate.FullName));
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var items = await _applicantRepository.GetAllAsync();

            var candiates = await _candidateRepository.GetAllAsync();

            var applicants = items.Select(a =>
            {
                var candidate = candiates.FirstOrDefault(c => c.Id == a.CandidateId);

                return a.AsDto(candidate.FullName);
            });
            return Ok(applicants);
        }
        [HttpGet("jobid/{id:guid}")]
        public async Task<IActionResult> GetByJobIdAsync(Guid id)
        {
            var items = await _applicantRepository.GetAllAsync(a => a.JobId == id);

            var candiates = await _candidateRepository.GetAllAsync();

            var applicants = items.Select(a =>
            {
                var candidate = candiates.FirstOrDefault(c => c.Id == a.CandidateId);

                return a.AsDto(candidate.FullName);
            });
            return Ok(applicants);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Applicant applicant)
        {
            var current = DateTime.UtcNow;
            applicant.CreatedDate = current;
            applicant.ModifiedDate = current;
            try
            {
                await _applicantRepository.CreateAsync(applicant);
                //await _publishEndpoint.Publish(new ApplicantCreated
                //(
                //    Applicant.Id, Applicant.Name, Applicant.UsageInMonth, Applicant.NoOfApplicantPosted, Applicant.NoOfCandidateInterview, Applicant.Price
                //));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = applicant.Id }, applicant);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, Applicant applicant)
        {
            var item = await _applicantRepository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _applicantRepository.UpdateAsync(applicant);
                // await _publishEndpoint.Publish(new ApplicantUpdated
                //(
                //    Applicant.Id, Applicant.Name, Applicant.UsageInMonth, Applicant.NoOfApplicantPosted, Applicant.NoOfCandidateInterview, Applicant.Price
                //));
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(applicant);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _applicantRepository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _applicantRepository.DeleteAsync(id);

                //await _publishEndpoint.Publish(new ApplicantDeleted(id));
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item);
        }
    }
}