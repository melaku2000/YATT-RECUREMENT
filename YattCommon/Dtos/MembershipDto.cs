using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Dtos
{
    public record MembershipDto(Guid Id, string? Name, int UsageInMonth, int NoOfJobPosted, decimal Price, DateTime CreatedDate, DateTime ModifiedDate);
}
