using CandidateService.Models;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Dtos;

namespace CandidateService.Controllers
{
    [Route("api/candidate/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        public EducationController(IRepository<Education> educationRepository, IRepository<Candidate> candidateRepository)
        {
            _educationRepository = educationRepository;
            _candidateRepository = candidateRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var education = await _educationRepository.GetAsync(id);

            if (education == null) return NotFound();

            return Ok(education.AsDto());
        }
        [HttpGet("list/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok((await _educationRepository.GetAllAsync(a=>a.CandidateId==id)).Select(a=>a.AsDto()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(EducationDto item)
        {
            var candidate=await _candidateRepository.GetAsync(e=>e.Id==item.CandidateId);

            if(candidate == null) return NotFound($"Candidate with id : {item.CandidateId} could not found");

            var current = DateTime.UtcNow;
            var education = new Education
            {
                CandidateId = candidate.Id,
                AcademyName = item.AcademyName,
                Level = item.Level,
                FieldOfStudy = item.FieldOfStudy,
                Grade = item.Grade,
                Completed = item.Completed,
                StartDate = item.StartDate,
                ComplitionDate = item.ComplitionDate,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            };
            item.CreatedDate= current;
            item.ModifiedDate= current;
            try
            {
                await _educationRepository.CreateAsync(education);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync),new { id = item.Id }, education.AsDto());
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id,EducationDto item)
        {
            var education = await _educationRepository.GetAsync(id);

            if (education == null) return NotFound();

            education.AcademyName = item.AcademyName;
            education.Level = item.Level;
            education.FieldOfStudy = item.FieldOfStudy;
            education.Grade = item.Grade;
            education.Completed = item.Completed;
            education.StartDate= item.StartDate;
            education.ComplitionDate= item.ComplitionDate;
            education.ModifiedDate=DateTime.UtcNow;

            await _educationRepository.UpdateAsync(education); 
            return Ok(education.AsDto());
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            var education = await _educationRepository.GetAsync(id);

            if (education == null) return NotFound();

            await _educationRepository.DeleteAsync(id);
            return Ok(education.AsDto());
        }
    }
}
