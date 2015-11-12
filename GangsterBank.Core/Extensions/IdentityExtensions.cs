namespace GangsterBank.Core.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Security.Principal;

    using Microsoft.AspNet.Identity;

    public static class IdentityExtensions
    {
        public static int GetUserEntityId(this IIdentity identity)
        {
            Contract.Requires<ArgumentNullException>(identity != null);
            string stringUserId = identity.GetUserId();
            int userId;
            if (!int.TryParse(stringUserId, out userId))
            {
                throw new ArgumentException("Identity has invalid UserId");
            }

            return userId;
        }
    }
}