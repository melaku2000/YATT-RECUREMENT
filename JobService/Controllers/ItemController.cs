using JobService.Models;
using Microsoft.AspNetCore.Mvc;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace JobService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<SubscribeModel> _subscribeRepository;
        private readonly IRepository<Duty> _dutyRepository;
        private readonly IRepository<Qualification> _qualiRepository;
        private readonly IRepository<Description> _descriptionRepository;

        private readonly IYattHttpClient<JobContract> _httpClient;

        public ItemController(IRepository<Job> repository, IRepository<SubscribeModel> subscribeRepository, IYattHttpClient<JobContract> httpClient,
             IRepository<Duty> dutyRepository, IRepository<Qualification> qualiRepository, IRepository<Description> descriptionRepository)
        {
            _jobRepository = repository;
            _subscribeRepository = subscribeRepository;
            _dutyRepository = dutyRepository;
            _qualiRepository = qualiRepository;
            _descriptionRepository = descriptionRepository;

            _httpClient= httpClient;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetAsync(id);

            if (job == null) return NotFound($"Job with id : {id} could not found");

            return Ok(job.AsDto());
        }
        [HttpGet("detail/{id:guid}")]
        public async Task<IActionResult> GetDetailByIdAsync(Guid id)
        {
            var item = await _jobRepository.GetAsync(id);

            if (item == null) return NotFound($"Job with id : {id} could not found");

            var duties = (await _dutyRepository.GetAllAsync(a => a.JobId == item.Id)).Select(a=>a.AsDto());
            var qualifications = (await _qualiRepository.GetAllAsync(a => a.JobId == item.Id)).Select(a => a.AsDto());
            var descriptions = (await _descriptionRepository.GetAllAsync(a => a.JobId == item.Id)).Select(a => a.AsDto());

            return Ok(item.AsDto(duties.ToList(), qualifications.ToList(), descriptions.ToList()));
        }

        [HttpGet("subscriptionid/{id:guid}")]
        public async Task<IActionResult> GetByCompanyIdAsync(Guid id)
        {
            var jobs = (await _jobRepository.GetAllAsync(a=>a.SubscribeId==id)).Select(a => a.AsDto());
            return Ok(jobs);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAsync(List<KeyValuePair<string,string>> parameters)
        {
            var jobs=(await _jobRepository.GetAllAsync()).Select(a=>a.AsDto());
            return Ok(jobs);
        }
      
        [HttpPost]
        public async Task<IActionResult> CreateAsync(JobDto item)
        {
            var current = DateTime.UtcNow;
            var subscription = await _subscribeRepository.GetAsync(a => a.Id == item.SubscribeId);
            if (subscription == null) return NotFound($"Subscription with {item.SubscribeId} could not found");
           
            var job = new Job
            {
                Id = item.Id,
                SubscribeId = item.SubscribeId,
                JobType = item.JobType,
                Level = item.Level,
                Employment = item.Employment,
                Salary = item.Salary,
                Location = item.Location,
                Title = item.Title,
                Description = item.Description,
                DeadlineDate = item.DeadlineDate,
                CreatedDate = current,
                ModifiedDate = current,
                Status = item.Status
            }; 
            try
            {
                await _jobRepository.CreateAsync(job);

                await _httpClient.PostAsync("http://host.docker.internal:19010/api/contracts/job",
                  new JobContract(job.Id,job.Title,job.JobType,job.Level,
                    job.Employment,job.Salary,job.Description,job.Location,job.DeadlineDate,job.ModifiedDate,job.Status));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = job.Id }, job);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, JobDto item)
        {
            var job = await _jobRepository.GetAsync(id);

            if (job == null) return NotFound($"Job with id : {id} could not found");

            job.Title= item.Title;
            job.JobType= item.JobType;
            job.Level= item.Level;
            job.Employment= item.Employment;
            job.Salary= item.Salary;
            job.Location= item.Location;
            job.Description= item.Description;
            job.DeadlineDate= item.DeadlineDate;
            job.Status=item.Status;
            job.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _jobRepository.UpdateAsync(job);
                
                await _httpClient.PostAsync("http://host.docker.internal:19010/api/contracts/job",
                     new JobContract(job.Id, job.Title, job.JobType, job.Level,
                       job.Employment, job.Salary, job.Description, job.Location, job.DeadlineDate, job.ModifiedDate, job.Status));
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(job.AsDto());
        }
      
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _jobRepository.GetAsync(id);

            if (item == null) return NotFound();
            try
            {
                await _jobRepository.DeleteAsync(id);
                
                await _httpClient.DeleteAsync("http://host.docker.internal:19010/api/contracts/job", id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(item);
        }
    }
}