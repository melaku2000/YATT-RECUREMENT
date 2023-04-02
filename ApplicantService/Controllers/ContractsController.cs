using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon.Contracts;
using YattCommon;
using ApplicantService.Models;

namespace ApplicantService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IRepository<CandidateModel> _candidateRepository;
        public IRepository<JobModel> _jobRepository { get; }
        public ContractsController(IRepository<CandidateModel> candidateRepository, IRepository<JobModel> jobRepository)
        {
            _candidateRepository = candidateRepository;
            _jobRepository = jobRepository;
        }


        [HttpPost("candidate")]
        public async Task<IActionResult> CreateCandidateAsync(CandidateContract item)
        {

            var candidate = await _candidateRepository.GetAsync(item.Id);

            try
            {
                if (candidate == null)
                {
                    candidate = new CandidateModel()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        FullName = item.FullName,
                        ModifiedDate = item.ModifiedDate
                    };

                    await _candidateRepository.CreateAsync(candidate);
                }
                else
                {
                    candidate.Email = item.Email;
                    candidate.FullName = item.FullName;
                    candidate.ModifiedDate = item.ModifiedDate;

                    await _candidateRepository.UpdateAsync(candidate);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(candidate);
        }

        [HttpDelete("candidate/{id:guid}")]
        public async Task<IActionResult> DeleteCandidateAsync(Guid id)
        {

            var candidate = await _candidateRepository.GetAsync(id);

            if (candidate == null) return NotFound($"The item with id : {id} could not found");

            try
            {
                await _candidateRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(candidate);
        }

        [HttpPost("job")]
        public async Task<IActionResult> CreateJobAsync(JobContract item)
        {

            var job = await _jobRepository.GetAsync(item.Id);

            try
            {
                if (job == null)
                {
                    job = new JobModel()
                    {
                        Title = item.Title, 
                        JobType = item.JobType,
                        Level = item.Level,
                        Employment = item.Employment,
                        Salary = item.Salary,
                        Description = item.Description,
                        Location = item.Location,
                        DeadlineDate = item.DeadlineDate,
                        ModifiedDate = item.ModifiedDate,
                        Status = item.Status
                    };

                    await _jobRepository.CreateAsync(job);
                }
                else
                {
                    job.Title = item.Title;
                    job.JobType = item.JobType;
                    job.Level = item.Level;
                    job.Employment = item.Employment;
                    job.Salary = item.Salary;
                    job.Description = item.Description;
                    job.Location = item.Location;
                    job.DeadlineDate = item.DeadlineDate;
                    job.Status = item.Status;
                    job.ModifiedDate = item.ModifiedDate;

                    await _jobRepository.UpdateAsync(job);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(job);
        }

        [HttpDelete("job/{id:guid}")]
        public async Task<IActionResult> DeleteJobAsync(Guid id)
        {

            var catagory = await _jobRepository.GetAsync(id);

            if (catagory == null) return NotFound($"The item with id : {id} could not found");

            try
            {
                await _jobRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(catagory);
        }
    }
}
