using YattCommon.Contracts;
using YattCommon.Dtos;
using YattCommon.Enums.Extensions;

namespace CandidateService.Models
{
    public static class Extensions
    {
        public static CandidateDto AsDto(this Candidate dto,UserModel user)
        {
            return new CandidateDto
            {
                Id = dto.Id,
                Email= user.Email,
                EmailConfirmed= user.EmailConfirmed,
                PhoneConfirmed=dto.PhoneConfirmed,
                FirstName = dto.FirstName,
                FatherName = dto.FatherName,
                Gender = dto.Gender,
                DoBirth = dto.DoBirth,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber, 
                ShowGender = dto.ShowGender,
                ShowDoBirth = dto.ShowDoBirth,
                ShowAddress = dto.ShowAddress,
                ShowPhone = dto.ShowPhone,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
            };
        }

        public static EducationDto AsDto(this Education dto)
        {
            return new EducationDto
            {
                Id = dto.Id,
                CandidateId = dto.CandidateId,
                AcademyName = dto.AcademyName,
                Level = dto.Level,
                LevelName = dto.Level.GetStringValue(),
                FieldOfStudy = dto.FieldOfStudy,
                Grade = dto.Grade,
                StartDate = dto.StartDate,
                ComplitionDate = dto.ComplitionDate,
                Completed = dto.Completed,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate
            };
        }
        public static ExperianceDto AsDto(this Experiance dto)
        {
            return new ExperianceDto
            {
                Id = dto.Id,
                CandidateId = dto.CandidateId,
                CompanyName = dto.CompanyName,
                Occupation = dto.Occupation,
                Level = dto.Level,
                LevelName = dto.Level.GetStringValue(),
                HiredDate = dto.HiredDate,
                LastDate = dto.LastDate,
                StillWorking = dto.StillWorking,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate
            };
        }
        public static UserLanguageDto AsDto(this UserLanguage dto,LanguageModel language)
        {
            return new UserLanguageDto
            {
                Id = dto.Id,
                CandidateId = dto.CandidateId,
                LanguageId = dto.LanguageId,
                LanguageName = language.Name,
                Level = dto.Level,
                LevelName=dto.Level.GetStringValue(),
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate
            };
        }

        public static CandidateDetailDto AsDto(this Candidate dto,UserModel user,List<ExperianceDto> experiances,
            List<EducationDto> educations,List<UserLanguageDto> languages)
        {
            return new CandidateDetailDto
            {
                Id = dto.Id, 
                Email=user.Email,
                EmailConfirmed= user.EmailConfirmed,
                FirstName = dto.FirstName,
                FatherName = dto.FatherName,
                Gender = dto.Gender,
                DoBirth = dto.DoBirth, 
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                PhoneConfirmed=dto.PhoneConfirmed, 
                ShowGender = dto.ShowGender,
                ShowDoBirth = dto.ShowDoBirth,
                ShowAddress = dto.ShowAddress,
                ShowPhone = dto.ShowPhone,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
                Educations = educations,
                Experiances = experiances,
                Languges = languages
            };
        }

        public static CandidateContract AsContract(this Candidate item,string email,string fullName)
        {
            return new CandidateContract(item.Id, email, fullName, item.ModifiedDate);
        }

    }
}
