using YattCommon;
using YattCommon.Enums;

namespace CatagoryService.Models
{
    public class Catagory:IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
