using MongoDB.Bson.Serialization.Attributes;
using YattCommon;

namespace IdentityService.Models
{
    public class RefreshToken : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
