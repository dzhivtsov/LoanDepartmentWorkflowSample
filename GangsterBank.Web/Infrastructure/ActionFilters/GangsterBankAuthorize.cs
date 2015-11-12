namespace GangsterBank.Web.Infrastructure.ActionFilters
{
    #region

    using System.Web.Mvc;

    using GangsterBank.Domain.Entities.Membership;

    #endregion

    public class GangsterBankAuthorizeAttribute : AuthorizeAttribute
    {
        #region Constants

        private const string RolesSeparator = ",";

        #endregion

        #region Constructors and Destructors

        public GangsterBankAuthorizeAttribute()
        {
        }

        public GangsterBankAuthorizeAttribute(params Role[] roles)
        {
            string roleNames = string.Join(RolesSeparator, roles);
            this.Roles = roleNames;
        }

        #endregion
    }
}