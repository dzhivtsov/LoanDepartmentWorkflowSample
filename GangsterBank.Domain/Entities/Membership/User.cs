namespace GangsterBank.Domain.Entities.Membership
{
    #region

    using GangsterBank.Domain.Entities.Base;

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public abstract class User : IdentityUser<int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity>, 
                        IEntity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}