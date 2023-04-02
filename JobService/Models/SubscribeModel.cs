using YattCommon;
using YattCommon.Enums;

namespace JobService.Models
{
    public class SubscribeModel : IEntity
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }=string.Empty;
        public int UsageInMonth { get; set; }
        public int NoOfJobPosted { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
