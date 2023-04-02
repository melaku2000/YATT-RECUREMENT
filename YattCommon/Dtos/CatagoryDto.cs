using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YattCommon.Dtos
{
    public record CatagoryDto (Guid Id, string Title, DateTime CreatedDate, DateTime ModifiedDate );
    
}
