using System;
using GangsterBank.Web.Infrastructure.Contexts;

namespace GangsterBank.Web.Controllers
{
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Credits;

    public class BaseController : Controller
    {
        protected readonly IUserContext _userContext;

        public BaseController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        protected int ResolveClientId(int? clientId)
        {
            if (this._userContext.IsClient)
            {
                return this._userContext.User.Id;
            }
            if (!clientId.HasValue)
            {
                return this._userContext.User.Id;
            }
            return clientId.Value;
            /*
            if (this._userContext.IsAdministrator || this._userContext.IsOperator)
            {
                if (this._userContext.IsClient)
                {
                    return this._userContext.User.Id;
                }
                if (!clientId.HasValue)
                {
                    throw new ArgumentNullException("clientId");
                }
                return clientId.Value;
            }

            */

            throw new ArgumentOutOfRangeException("clientId");
        }
    }
}