using YattCommon;

namespace CompanyService.Models
{
    public class CatagoryModel:IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
