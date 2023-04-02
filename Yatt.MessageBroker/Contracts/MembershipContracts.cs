namespace Yatt.MessageBroker.Contracts
{
    public record MembershipCreated(Guid Id,string? Name,int UsageInMonth,int NoOfJobPosted,int NoOfCandidateInterview, decimal Price);
    public record MembershipUpdated(Guid Id, string? Name, int UsageInMonth, int NoOfJobPosted, int NoOfCandidateInterview, decimal Price);
    public record MembershipDeleted(Guid Id);
}
