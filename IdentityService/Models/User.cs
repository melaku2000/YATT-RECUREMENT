using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using YattCommon;

namespace IdentityService.Models
{
    public class User:IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
       
        public bool EmailConfirmed { get; set; }
       
        public int LockCount { get; set; }

        public DateTime LastLogin { get; set; }

        public string Role { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
