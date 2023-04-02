using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Contracts
{
    public record CatagoryContract(Guid Id, string Title, DateTime ModifiedDate);
}
