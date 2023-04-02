using YattCommon.Enums;
using YattCommon;

namespace CompanyService.Models
{
    public class Company : IEntity
    {
        public Guid Id { get; set; }
        public Guid CatagoryId { get; set; }
        public string TinNumber { get; set; }=string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string WebUrl { get; set; } = string.Empty;
        public bool UseTrial { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public UserStatus Status { get; set; }
    }
}
