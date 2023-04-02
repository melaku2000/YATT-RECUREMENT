using YattCommon;
using YattCommon.Enums;

namespace IdentityService.Models
{
    public class UserToken : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
