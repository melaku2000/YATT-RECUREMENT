using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Contracts
{
    public record SubscriptionContract(Guid Id, Guid CompanyId,string CompanyName, int UsageInMonth, int NoOfJobPosted, 
        SubscriptionStatus Status, DateTime ModifiedDate);
}
