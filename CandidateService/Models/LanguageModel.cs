using YattCommon;

namespace CandidateService.Models
{
    public class LanguageModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
    }
}
