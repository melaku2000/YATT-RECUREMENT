using YattCommon.Contracts;
using YattCommon.Dtos;

namespace CompanyService.Models
{
    public static class Extensions
    {
        public static CompanyDto AsDto(this Company item,string catagoryName)
        {
            return new CompanyDto(item.Id, item.CatagoryId,catagoryName,item.TinNumber,item.CompanyName, item.CompanyPhone,item.ContactName,
                item.ContactPhone,item.Address,item.Description,item.WebUrl,item.CreatedDate,item.ModifiedDate,item.UseTrial,item.Status);
        }
        public static Company GetItem(this CompanyDto item)
        {
            return new Company
            {
                Id = item.Id,
                CatagoryId = item.CatagoryId,
                TinNumber = item.TinNumber,
                CompanyName = item.CompanyName,
                CompanyPhone = item.CompanyPhone,
                ContactName = item.ContactName,
                ContactPhone = item.ContactPhone,
                Address = item.Address,
                Description = item.Description,
                WebUrl = item.WebUrl,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,
                Status = item.Status,
                UseTrial = item.UseTrial,
            };
        }
        public static CompanyContract AsContract(this Company item)
        {
            return new CompanyContract(item.Id,item.CompanyName,item.UseTrial,item.Status, item.ModifiedDate);
        }
    }
}
