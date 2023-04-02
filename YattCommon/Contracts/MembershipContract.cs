using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Contracts
{
    public record MembershipContract(Guid Id, string Name, int UsageInMonth, int NoOfJobPosted, decimal Price, DateTime ModifiedDate);
}
