using YattCommon;
using YattCommon.Enums;

namespace ApplicantService.Models
{
    public class CandidateModel : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; }= string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
