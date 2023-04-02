using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum SubscriptionStatus
    {
        [StringValue("Pending")] Pending=301,
        [StringValue("Approved")] Approved=302,
        [StringValue("Started")] Started=303,
        [StringValue("Canceled")] Canceled = 304,
    }
}
