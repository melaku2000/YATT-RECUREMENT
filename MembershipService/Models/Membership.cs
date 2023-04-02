using YattCommon;

namespace MembershipService.Models
{
    public class Membership : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public int UsageInMonth { get; set; }
        public int NoOfJobPosted { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } 
    }
}
