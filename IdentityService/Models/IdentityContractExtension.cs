
using YattCommon.Contracts;

namespace IdentityService.Models
{
    public static class IdentityContractExtension
    {
        public static IdentityContract AsContract(this User user)
        {
            return new IdentityContract(user.Id, user.Email, user.EmailConfirmed, user.ModifiedDate);
        }
    }
}
