namespace GangsterBank.Web.Infrastructure.Helpers.Membership
{
    #region

    using System;
    using System.Diagnostics.Contracts;
    using System.Web;

    using GangsterBank.BusinessLogic;
    using GangsterBank.BusinessLogic.Contracts.Membership;
    using GangsterBank.BusinessLogic.Membership;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity;

    #endregion

    public class CurrentUserRetriever : ICurrentUserRetriever
    {
        #region Fields

        private readonly HttpContextBase httpContext;

        private readonly IUserService userService;

        #endregion

        #region Constructors and Destructors

        public CurrentUserRetriever(HttpContextBase httpContext, IUserService userService)
        {
            Contract.Requires<ArgumentNullException>(httpContext != null);
            Contract.Requires<ArgumentNullException>(userService != null);
            this.httpContext = httpContext;
            this.userService = userService;
        }

        #endregion

        #region Public Methods and Operators

        public User GetCurrentUser()
        {
            int userId = this.httpContext.User.Identity.GetUserEntityId();
            User user = this.userService.FindByIdAsync(userId).Result;
            return user;
        }

        #endregion
    }
}