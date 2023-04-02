using YattCommon;
using YattCommon.Enums;

namespace CandidateService.Models
{
    public class UserLanguage : IEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid LanguageId { get; set; }
        public ExperianceLevel Level { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
