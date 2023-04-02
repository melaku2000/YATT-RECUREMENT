using YattCommon.Contracts;
using YattCommon.Dtos;

namespace IdentityService.Models
{
    public static class Extensions
    {
        public static UserDto AsDto(this User item)
        {
            return new UserDto(item.Id, item.Email,item.EmailConfirmed,item.LockCount,item.LastLogin,item.Role, item.CreatedDate,item.ModifiedDate);
        }

        public static IdentityContract AsContract(this User item)
        {
            return new IdentityContract(item.Id, item.Email,item.EmailConfirmed, item.ModifiedDate);
        }
    }
}
