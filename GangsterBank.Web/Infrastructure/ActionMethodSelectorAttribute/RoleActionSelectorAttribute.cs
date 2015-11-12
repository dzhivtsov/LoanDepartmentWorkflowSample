using System;

namespace GangsterBank.Web.Infrastructure.ActionMethodSelectorAttribute
{
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using System.Web.Mvc;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Membership;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RoleActionSelectorAttribute : ActionMethodSelectorAttribute
    {
        private readonly string role;

        public RoleActionSelectorAttribute(Role role)
        {
            this.role = role.ToString();
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var httpContext = controllerContext.HttpContext;
            //Contract.Requires<ArgumentNullException>(httpContext.IsNotNull());
            var user = httpContext.User;
            return user.Identity.IsAuthenticated && user.IsInRole(this.role);
        }
    }
}