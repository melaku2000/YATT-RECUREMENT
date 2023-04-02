using YattCommon;

namespace SubscriptionService.Models
{
    public class MembershipModel : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int UsageInMonth { get; set; }
        public int NoOfJobPosted { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
