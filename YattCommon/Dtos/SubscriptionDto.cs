using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Dtos
{
    public record SubscriptionDto(Guid Id, Guid CompanyId,string CompanyName, Guid MembershipId, int UsageInMonth, int NoOfJobPosted, 
        decimal Price,SubscriptionStatus Status,string StatusName,DateTime CreatedDate, DateTime ModifiedDate);
   
}
