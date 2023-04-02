using YattCommon;
using YattCommon.Enums;

namespace ApplicantService.Models
{
    public class JobModel : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public JobType JobType { get; set; }
        public ExperianceLevel Level { get; set; }
        public Employment Employment { get; set; }
        public decimal Salary { get; set; }
        public string Description { get; set; }=string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime DeadlineDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ServiceStatus Status { get; set; }
    }
}
