using CandidateService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Yatt.HttpClientService;
using YattCommon;
using YattCommon.Contracts;
using YattCommon.Dtos;

namespace CandidateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<LanguageModel> _languageRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<Experiance> _experianceRepository;
        private readonly IRepository<UserLanguage> _userLanguageRepository;
        private readonly IRepository<UserModel> _userRepository;

        private readonly IYattHttpClient<CandidateContract> _httpClient;

        public ItemController(IRepository<Candidate> repository, IRepository<UserModel> userRepository, IRepository<LanguageModel> languageRepository,
            IRepository<Experiance> experianceRepository, IRepository<Education> educationRepository, IRepository<UserLanguage> userLanguageRepository,
            IYattHttpClient<CandidateContract> httpClient)
        {
            _candidateRepository = repository;
            _userRepository = userRepository;
            _educationRepository= educationRepository;
            _experianceRepository= experianceRepository;
            _userLanguageRepository= userLanguageRepository;
            _languageRepository= languageRepository;

            _httpClient= httpClient;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetAsync(id);

            if (candidate == null) return NotFound();
            var user = await _userRepository.GetAsync(a => a.Id == candidate.Id);
            return Ok(candidate.AsDto(user));
        }
        [HttpGet("detail/{id:guid}")]
        public async Task<IActionResult> GetDetailByIdAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetAsync(id);

            if (candidate == null) return NotFound($"Candidate with id : {id} could not found");

            var user = await _userRepository.GetAsync(a => a.Id == id);

            var experiances = (await _experianceRepository.GetAllAsync(a => a.CandidateId == id)).Select(a=>a.AsDto());
            var educations=(await _educationRepository.GetAllAsync(a=>a.CandidateId == id)).Select(a=>a.AsDto());
            var languges = await _languageRepository.GetAllAsync();

            var userLanguages=(await _userLanguageRepository.GetAllAsync(a=>a.CandidateId == id)).Select(x => {
                var lang = languges.SingleOrDefault(lan => lan.Id == x.LanguageId);
                return x.AsDto(lang);
            });
             
            return Ok(candidate.AsDto(user:user, experiances:experiances.ToList(),educations:educations.ToList(),languages:userLanguages.ToList()));
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users=await _userRepository.GetAllAsync();
            var candidates= (await _candidateRepository.GetAllAsync())
                .Select(a =>
                {
                    var user = users.FirstOrDefault(x => x.Id == a.Id);

                    return a.AsDto(user!);
                });
            return Ok(candidates);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CandidateDto item)
        {
            var user = await _userRepository.GetAsync(a => a.Id == item.Id);

            if (user == null) return NotFound($"User with id : {item.Id} could not found");

            if (!user.EmailConfirmed) return Unauthorized("Email confermation is required");

            var current = DateTime.UtcNow;
            var candidate = new Candidate()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                FatherName = item.FatherName,
                PhoneNumber = item.PhoneNumber,
                PhoneConfirmed=false,
                Gender = item.Gender,  
                DoBirth = item.DoBirth,
                Address = item.Address,
                ShowPhone = item.ShowPhone,
                ShowGender = item.ShowGender,
                ShowDoBirth = item.ShowDoBirth,
                ShowAddress = item.ShowAddress,
                CreatedDate = current,
                ModifiedDate = current
            };
            
            try
            {
                await _candidateRepository.CreateAsync(candidate);

                await _httpClient.PostAsync("http://host.docker.internal:19010/api/contracts/candidate",
                    new CandidateContract(candidate.Id,user.Email,$"{candidate.FirstName} {candidate.FatherName}",candidate.ModifiedDate));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetByIdAsync),new { id = candidate.Id },candidate.AsDto(user) );
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id,CandidateDto item)
        {
            var candidate = await _candidateRepository.GetAsync(id);

            if (candidate == null) return NotFound($"Candidate with id : {id} could not found");
           
            candidate.FirstName=item.FirstName;
            candidate.FatherName=item.FatherName;
            candidate.PhoneNumber=item.PhoneNumber;
            candidate.PhoneConfirmed=item.PhoneConfirmed;
            candidate.Gender=item.Gender;   
            candidate.DoBirth=item.DoBirth;
            candidate.Address=item.Address;
            candidate.ShowPhone=item.ShowPhone;
            candidate.ShowGender=item.ShowGender;
            candidate.ShowDoBirth=item.ShowDoBirth;
            candidate.ShowAddress=item.ShowAddress;
            candidate.ModifiedDate=DateTime.UtcNow;

            await _candidateRepository.UpdateAsync(candidate); 
            var user = await _userRepository.GetAsync(a => a.Id == item.Id);

            await _httpClient.PostAsync("http://host.docker.internal:19010/api/contracts/candidate",
                   new CandidateContract(candidate.Id, user.Email, $"{candidate.FirstName} {candidate.FatherName}", candidate.ModifiedDate));

            return Ok(candidate.AsDto(user));
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            var candidate = await _candidateRepository.GetAsync(id);

            if (candidate == null) return NotFound($"Candidate with id : {id} could not found");

            await _candidateRepository.DeleteAsync(id);
            
            await _httpClient.DeleteAsync("http://host.docker.internal:19010/api/contracts/candidate", id.ToString());
          
            return Ok(candidate.AsDto(new UserModel()));
        }
    }
}
