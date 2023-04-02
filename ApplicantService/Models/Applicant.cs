using YattCommon;
using YattCommon.Enums;

namespace ApplicantService.Models
{
    public class Applicant : IEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
        public ServiceStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
