using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum Employment
    {
        [StringValue("Permanent")] Permanent=411,
        [StringValue("Contract")] Contract=412,
    }
}
