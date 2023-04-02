using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Dtos
{
    public record CompanyDto(Guid Id, Guid CatagoryId, string CatagoryName, string TinNumber, string CompanyName,
        string CompanyPhone, string ContactName, string ContactPhone, string Address, string Description, string WebUrl, DateTime CreatedDate,
        DateTime ModifiedDate, bool UseTrial, UserStatus Status);
}
