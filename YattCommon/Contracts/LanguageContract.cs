using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Contracts
{
    public record LanguageContract(Guid Id, string Name, DateTime ModifiedDate);
}
