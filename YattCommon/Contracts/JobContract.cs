using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Contracts
{
    public record JobContract(Guid Id,string? Title, JobType JobType, ExperianceLevel Level, Employment Employment,
        decimal Salary, string? Description, string? Location, DateTime DeadlineDate,
        DateTime ModifiedDate, ServiceStatus Status);
}
