namespace GangsterBank.Web.Infrastructure.Helpers.Membership
{
    using GangsterBank.Domain.Entities.Membership;

    public interface ICurrentUserRetriever
    {
        User GetCurrentUser();
    }
}