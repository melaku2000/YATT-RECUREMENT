using CandidateService.Models;
using Microsoft.AspNetCore.Mvc;
using YattCommon;
using YattCommon.Dtos;

namespace CandidateService.Controllers
{
    [Route("api/candidate/[controller]")]
    [ApiController]
    public class ExperianceController : ControllerBase
    {
        private readonly IRepository<Experiance> _experianceRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        public ExperianceController(IRepository<Experiance> experianceRepository, IRepository<Candidate> candidateRepository)
        {
            _experianceRepository = experianceRepository;
            _candidateRepository = candidateRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var experiance = await _experianceRepository.GetAsync(id);

            if (experiance == null) return NotFound();

            return Ok(experiance.AsDto());
        }
        [HttpGet("list/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok((await _experianceRepository.GetAllAsync(a=>a.CandidateId==id)).Select(a=>a.AsDto()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ExperianceDto item)
        {
            var candidate=await _candidateRepository.GetAsync(e=>e.Id== item.CandidateId);

            if(candidate == null) return NotFound($"Candidate with id : {item.CandidateId} could not found");

            var current = DateTime.UtcNow;
            var experiance = new Experiance
            {
                CandidateId = item.CandidateId,
                CompanyName = item.CompanyName,
                Level = item.Level,
                Occupation = item.Occupation,
                HiredDate = item.HiredDate,
                LastDate = item.LastDate,
                StillWorking = item.StillWorking,
                CreatedDate = current,
                ModifiedDate = current
            };
           
            try
            {
                await _experianceRepository.CreateAsync(experiance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync),new { id = experiance.Id }, experiance.AsDto());
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id,ExperianceDto item)
        {
            var experiance = await _experianceRepository.GetAsync(id);

            if (experiance == null) return NotFound();

            experiance.CompanyName= item.CompanyName;
            experiance.Level=item.Level;
            experiance.Occupation= item.Occupation;
            experiance.HiredDate= item.HiredDate;
            experiance.LastDate= item.LastDate;
            experiance.StillWorking= item.StillWorking;
            experiance.ModifiedDate=DateTime.UtcNow;

            await _experianceRepository.UpdateAsync(experiance); 
            return Ok(experiance.AsDto());
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            var experiance = await _experianceRepository.GetAsync(id);

            if (experiance == null) return NotFound();

            await _experianceRepository.DeleteAsync(id);
            return Ok(experiance.AsDto());
        }
    }
}
