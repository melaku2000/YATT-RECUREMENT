using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum ServiceStatus
    {
        [StringValue("Pending")] Pending = 101,
        [StringValue("Approved")] Approved = 102,
        [StringValue("Canceled")] Canceled = 103,
        [StringValue("Deleted")] Deleted = 104
    }
}
