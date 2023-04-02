using YattCommon;
using YattCommon.Enums;

namespace JobService.Models
{
    public class Qualification : IEntity
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string? Title { get; set; }
        public EducationLevel Level { get; set; } 
    }
}
