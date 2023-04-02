using JobService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Dtos;

namespace JobService.Controllers
{
    [Route("api/job/[controller]")]
    [ApiController]
    public class DutiesController : ControllerBase
    {
        private readonly IRepository<Duty> _dutyRepository;
        private readonly IRepository<Job> _jobRepository;
        public DutiesController(IRepository<Duty> dutyRepository, IRepository<Job> jobRepository) 
        {
            _dutyRepository = dutyRepository;
            _jobRepository = jobRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var duty = await _dutyRepository.GetAsync(id);

            if (duty == null) return NotFound($"Duty with id : {id} could not found");

            return Ok(duty.AsDto());
        }
        [HttpGet("list/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var duties = await _dutyRepository.GetAllAsync(a => a.JobId == id);
            return Ok(duties.Select(a=>a.AsDto()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DutyDto item)
        {
            var job = await _jobRepository.GetAsync(e => e.Id == item.JobId);

            if (job == null) return NotFound($"Job with id : {item.JobId} could not found");
            var duty=new Duty { JobId = job.Id, Detail=item.Detail};
            try
            {
                await _dutyRepository.CreateAsync(duty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = duty.Id }, duty.AsDto());
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, DutyDto dto)
        {
            var duty= await _dutyRepository.GetAsync(id);

            if (duty == null) return NotFound($"Duty with id : {id} could not found");

            duty.Detail= dto.Detail;
            await _dutyRepository.UpdateAsync(duty);
            return Ok(duty.AsDto());
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var duty = await _dutyRepository.GetAsync(id);

            if (duty == null) return NotFound();

            await _dutyRepository.DeleteAsync(id);
            return Ok(duty.AsDto());
        }
    }
}
