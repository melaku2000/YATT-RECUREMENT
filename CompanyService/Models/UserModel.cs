using YattCommon;

namespace CompanyService.Models
{
    public class UserModel : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } 
        public DateTime ModifiedDate { get; set; }
    }
}
