using YattCommon.Contracts;
using YattCommon.Dtos;

namespace MembershipService.Models
{
    public static class Extensions
    {
        public static MembershipDto AsDto(this Membership item)
        {
            return new MembershipDto(item.Id, item.Name,item.UsageInMonth,item.NoOfJobPosted,item.Price, item.CreatedDate, item.ModifiedDate);
        }
        public static Membership GetItem(this MembershipDto item)
        {
            return new Membership()
            {
                Id = item.Id,
                Name = item.Name,
                UsageInMonth = item.UsageInMonth,
                NoOfJobPosted = item.NoOfJobPosted,
                Price = item.Price,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            };
        }
        public static MembershipContract AsContract(this Membership item)
        {
            return new MembershipContract(item.Id, item.Name, item.UsageInMonth, item.NoOfJobPosted, item.Price, item.ModifiedDate);
        }
    }
}
