using YattCommon;
using YattCommon.Enums;

namespace CandidateService.Models
{
    public class Education : IEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? AcademyName { get; set; }
        public EducationLevel Level { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? Grade { get; set; }
        public bool Completed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ComplitionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
