using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum UserStatus
    {
        [StringValue("Pending")] Pending=121,
        [StringValue("Approved")] Approved=122,
        [StringValue("Denied")] Denied=123,
        [StringValue("Suspend")] Suspend=124,
    }
}
