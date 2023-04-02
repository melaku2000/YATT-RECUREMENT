using YattCommon;
using YattCommon.Enums;

namespace JobService.Models
{
    public class Job : IEntity
    {
        public Guid Id { get; set; }
        public Guid SubscribeId { get; set; } 
        public string? Title { get; set; }
        public JobType JobType { get; set; }
        public ExperianceLevel Level { get; set; }
        public Employment Employment { get; set; }
        public decimal Salary { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ServiceStatus Status { get; set; }
    }
}
