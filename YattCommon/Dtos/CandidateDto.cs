using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Dtos
{
    public class CandidateDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; } 
        public string? FirstName { get; set; }
        public string? FatherName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DoBirth { get; set; }
        public string? Address { get; set; }
        public bool ShowPhone { get; set; }
        public bool ShowGender { get; set; }
        public bool ShowDoBirth { get; set; }
        public bool ShowAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class EducationDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? AcademyName { get; set; }
        public EducationLevel Level { get; set; }
        public string? LevelName { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? Grade { get; set; }
        public bool Completed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ComplitionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class ExperianceDto 
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? CompanyName { get; set; }
        public ExperianceLevel Level { get; set; }
        public string? LevelName { get; set; }
        public string? Occupation { get; set; }
        public bool StillWorking { get; set; }
        public DateTimeOffset? HiredDate { get; set; }
        public DateTimeOffset? LastDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class UserLanguageDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public ExperianceLevel Level { get; set; }
        public string? LevelName { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class CandidateDetailDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? FatherName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DoBirth { get; set; }
        public string? Address { get; set; }
        public bool ShowPhone { get; set; }
        public bool ShowGender { get; set; }
        public bool ShowDoBirth { get; set; }
        public bool ShowAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<ExperianceDto> Experiances { get; set; } = null!;
        public ICollection<EducationDto> Educations { get; set; } = null!; 
        public ICollection<UserLanguageDto> Languges { get; set; } = null!;  
    }
}
