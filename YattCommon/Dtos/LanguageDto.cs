using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Dtos
{
    public record LanguageDto(Guid Id, string Name, DateTime CreatedDate, DateTime ModifiedDate);
}
