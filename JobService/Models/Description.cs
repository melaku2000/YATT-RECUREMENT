using YattCommon;

namespace JobService.Models
{
    public class Description : IEntity
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string? Detail { get; set; }
    }
}
