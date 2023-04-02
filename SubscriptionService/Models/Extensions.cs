using YattCommon.Contracts;
using YattCommon.Dtos;
using YattCommon.Enums.Extensions;

namespace SubscriptionService.Models
{
    public static class Extensions
    {
        public static SubscriptionDto AsDto(this Subscription item,string companyName)
        {
            return new SubscriptionDto(item.Id,item.CompanyId,companyName,item.MembershipId,item.UsageInMonth,item.NoOfJobPosted,item.Price,item.Status,item.Status.GetStringValue(), item.CreatedDate, item.ModifiedDate);
        }
        public static Subscription GetItem(this SubscriptionDto item)
        {
            return new Subscription
            {
                Id = item.Id,
                CompanyId = item.CompanyId,
                MembershipId= item.MembershipId, UsageInMonth= item.UsageInMonth, NoOfJobPosted= item.NoOfJobPosted, Price=item.Price,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate,
                Status = item.Status
            };
        }
        public static SubscriptionContract AsContract(this Subscription item, string companyName)
        {
            return new SubscriptionContract(item.Id, item.CompanyId, companyName, item.UsageInMonth, item.NoOfJobPosted, item.Status, item.ModifiedDate);
        }
    }
}
