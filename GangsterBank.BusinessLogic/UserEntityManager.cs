namespace GangsterBank.BusinessLogic
{
    #region

    using GangsterBank.DataAccess.Repositories;
    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity;

    #endregion

    public class UserEntityManager : UserManager<User, int>
    {
        #region Constructors and Destructors

        public UserEntityManager(IUserEntityStore store)
            : base(store)
        {
        }

        #endregion
    }
}