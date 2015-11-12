namespace GangsterBank.Domain.Entities.Membership
{
    #region

    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Base;
    using GangsterBank.Domain.Entities.Credits;

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class IdentityRoleEntity : IdentityRole<int, IdentityUserRoleEntity>, IEntity
    {
        #region Public Properties

        public virtual bool IsDeleted { get; set; }

        public virtual ICollection<LoanProductRequirements> LoanProductRequirements { get; set; }

        public virtual ICollection<LoanRequest> LoanRequests { get; set; } 

        #endregion
    }
}