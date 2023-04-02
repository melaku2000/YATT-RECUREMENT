using YattCommon;
using YattCommon.Enums;

namespace SubscriptionService.Models
{
    public class CompanyModel : IEntity
    {
        public Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public bool UseTrial { get; set; }
        public UserStatus Status { get; set; } 
        public DateTime ModifiedDate { get; set; }

    }
}
