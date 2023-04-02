using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum Gender
    {
        [StringValue("None")] None=201,
        [StringValue("Male")] Male=202,
        [StringValue("Female")] Female=203
    }
}
