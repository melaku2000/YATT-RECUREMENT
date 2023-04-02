using JobService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Dtos;

namespace JobService.Controllers
{
    [Route("api/job/[controller]")]
    [ApiController]
    public class DescriptionsController : ControllerBase
    {
        private readonly IRepository<Description> _descriptionRepository;
        private readonly IRepository<Job> _jobRepository;
        public DescriptionsController(IRepository<Description> descriptionRepository, IRepository<Job> jobRepository)  
        {
            _descriptionRepository = descriptionRepository;
            _jobRepository = jobRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var description = await _descriptionRepository.GetAsync(id);

            if (description == null) return NotFound($"Description with id : {id} could not found");

            return Ok(description.AsDto());
        }
        [HttpGet("list/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var descriptions= await _descriptionRepository.GetAllAsync(a=>a.JobId==id);
            return Ok(descriptions.Select(a=>a.AsDto()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DescriptionDto dto)
        {
            var job = await _jobRepository.GetAsync(e => e.Id == dto.JobId);

            if (job == null) return NotFound($"Job with id : {dto.JobId} could not found");

            var description=new Description { JobId=dto.JobId , Detail=dto.Detail};
            try
            {
                await _descriptionRepository.CreateAsync(description);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = description.Id }, description.AsDto());
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, DescriptionDto dto)
        {
            var description = await _descriptionRepository.GetAsync(id);

            if (description == null) return NotFound($"Description with id : {id} could not found");
         
            description.Detail = dto.Detail;

            await _descriptionRepository.UpdateAsync(description);
            return Ok(description.AsDto());
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var description = await _descriptionRepository.GetAsync(id);

            if (description == null) return NotFound($"Description with id : {id} could not found");

            await _descriptionRepository.DeleteAsync(id);
            return Ok(description.AsDto());
        }
    }
}
