using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum JobType
    {
        [StringValue("Full time")] FullTime=401,
        [StringValue("Part time")] PartTime=402,
        [StringValue("Others")] Others=403 
    }
}
