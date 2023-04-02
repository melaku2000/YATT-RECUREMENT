using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using YattCommon;

namespace LanguageService.Models 
{
    public class Language:IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
