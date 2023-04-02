using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Dtos
{
      public record UserDto(Guid Id, string? Email,bool EmailConfirmed,int LockCount, DateTime LastLogin, string Role, DateTime CreatedDate, DateTime ModifiedDate);
}
