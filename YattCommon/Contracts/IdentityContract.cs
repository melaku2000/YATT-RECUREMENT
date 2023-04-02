using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Contracts
{
    public record IdentityContract(Guid Id, string? Email, bool EmailConfirmed, DateTime ModifiedDate);
}
