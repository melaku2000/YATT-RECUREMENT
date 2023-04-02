using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Contracts
{
    public record CompanyContract(Guid Id, string CompanyName, bool UseTrial, UserStatus Status, DateTime ModifiedDate);
}
