namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity;

    public interface IUserEntityStore : IUserStore<User, int>
    {
        
    }
}