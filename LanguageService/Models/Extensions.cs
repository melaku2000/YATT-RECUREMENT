using YattCommon.Contracts;
using YattCommon.Dtos;

namespace LanguageService.Models
{
    public static class Extensions
    {
        public static LanguageDto AsDto(this Language item)
        {
            return new LanguageDto(item.Id, item.Name, item.CreatedDate, item.ModifiedDate);
        }
        public static LanguageContract AsContract(this Language item)
        {
            return new LanguageContract(item.Id, item.Name,item.ModifiedDate);
        }
        public static Language GetItem(this LanguageDto item)
        {
            return new Language()
            {
                Id = item.Id,
                Name = item.Name,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            };
        }
    }
}
