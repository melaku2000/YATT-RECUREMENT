using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Contracts
{
    public record CandidateContract(Guid Id, string Email, string FullName, DateTime ModifiedDate);
}
