using YattCommon;
using YattCommon.Enums;

namespace SubscriptionService.Models
{
    public class Subscription : IEntity
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid MembershipId { get; set; }
        public int UsageInMonth { get; set; }
        public int NoOfJobPosted { get; set; }
        public decimal Price { get; set; }
        public SubscriptionStatus Status { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
