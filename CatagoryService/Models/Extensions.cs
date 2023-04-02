using YattCommon.Contracts;
using YattCommon.Dtos;

namespace CatagoryService.Models
{
    public static class Extensions
    {
        public static CatagoryDto AsDto(this Catagory item)
        {
            return new CatagoryDto(item.Id, item.Title, item.CreatedDate, item.ModifiedDate);
        }
        public static Catagory GetItem(this CatagoryDto item)
        {
            return new Catagory()
            {
                Id = item.Id,
                Title = item.Title,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            };
        }
        public static CatagoryContract AsContract(this Catagory item)
        {
            return new CatagoryContract(item.Id, item.Title, item.ModifiedDate);
        }
    }
}
