namespace GangsterBank.DataAccess.Repositories
{
    #region

    using System.Data.Entity;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class UserEntityStore :
        UserStore<User, IdentityRoleEntity, int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity>, 
        IUserEntityStore
    {
        #region Constructors and Destructors

        public UserEntityStore(DbContext context)
            : base(context)
        {
        }

        #endregion
    }
}