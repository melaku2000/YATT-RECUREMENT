using YattCommon;
using YattCommon.Enums;

namespace CandidateService.Models
{
    public class Candidate:IEntity
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? FatherName { get; set; }
        public string? PhoneNumber { get; set; }
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
}
