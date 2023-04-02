using YattCommon;
using YattCommon.Enums;

namespace CandidateService.Models
{
    public class Experiance : IEntity
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string? CompanyName { get; set; }
        public ExperianceLevel Level { get; set; }
        public string? Occupation { get; set; }
        public bool StillWorking { get; set; } 
        public DateTimeOffset? HiredDate { get; set; }
        public DateTimeOffset? LastDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
