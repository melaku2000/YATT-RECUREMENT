using CandidateService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YattCommon;

namespace CandidateService.Controllers
{
    [Route("api/candidate/[controller]")]
    [ApiController]
    public class UserLanguageController : ControllerBase
    {
        private readonly IRepository<UserLanguage> _languageRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        public UserLanguageController(IRepository<UserLanguage> languageRepository, IRepository<Candidate> candidateRepository)
        {
            _languageRepository = languageRepository;
            _candidateRepository = candidateRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _languageRepository.GetAsync(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
        [HttpGet("list/{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _languageRepository.GetAllAsync(a=>a.CandidateId==id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserLanguage language)
        {
            var candidate=await _candidateRepository.GetAsync(e=>e.Id==language.CandidateId);

            if(candidate == null) return NotFound($"Candidate with id : {language.CandidateId} could not found");

            var current = DateTime.UtcNow;
            language.CreatedDate= current;
            language.ModifiedDate= current;
            try
            {
                await _languageRepository.CreateAsync(language);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync),new { id = language.Id }, language);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id,UserLanguage language)
        {
            var UserLanguageModel = await _languageRepository.GetAsync(id);

            if (UserLanguageModel == null) return NotFound();

            language.ModifiedDate=DateTime.UtcNow;

            await _languageRepository.UpdateAsync(language); 
            return Ok(language);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            var user = await _languageRepository.GetAsync(id);

            if (user == null) return NotFound();

            await _languageRepository.DeleteAsync(id);
            return Ok(user);
        }
    }
}
