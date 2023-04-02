using JobService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Dtos;

namespace JobService.Controllers
{
    [Route("api/job/[controller]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly IRepository<Qualification> _qualificationRepository;
        private readonly IRepository<Job> _jobRepository;
        public QualificationsController(IRepository<Qualification> qualificationRepository, IRepository<Job> jobRepository) 
        {
            _qualificationRepository = qualificationRepository;
            _jobRepository = jobRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var qualification = await _qualificationRepository.GetAsync(id);

            if (qualification == null) return NotFound($"Qualification with id : {id} could not found");

            return Ok(qualification.AsDto());
        }
        [HttpGet("list/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var qualifications = await _qualificationRepository.GetAllAsync(a => a.JobId == id);
            return Ok(qualifications.Select(a=>a.AsDto()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(QualificationDto dto)
        {
            var job = await _jobRepository.GetAsync(e => e.Id == dto.JobId);

            if (job == null) return NotFound($"Job with id : {dto.JobId} could not found");

            var qualification=new Qualification { JobId=dto.JobId, Level=dto.Level, Title=dto.Title };
            try
            {
                await _qualificationRepository.CreateAsync(qualification);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = qualification.Id }, qualification.AsDto());
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, QualificationDto dto)
        {
            var qualification = await _qualificationRepository.GetAsync(id);

            if (qualification == null) return NotFound($"Qualification with id : {id} could not found");

            qualification.Level = dto.Level;
            qualification.Title = dto.Title;

            await _qualificationRepository.UpdateAsync(qualification);
            return Ok(qualification.AsDto());
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var qualification = await _qualificationRepository.GetAsync(id);

            if (qualification == null) return NotFound($"Qualification with id : {id} could not found");

            await _qualificationRepository.DeleteAsync(id);
            return Ok(qualification.AsDto());
        }
    }
}
