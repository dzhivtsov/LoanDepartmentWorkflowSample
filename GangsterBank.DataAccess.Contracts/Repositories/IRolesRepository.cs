namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Membership;

    public interface IRolesRepository
    {
        #region Public Methods and Operators

        IEnumerable<IdentityRoleEntity> GetRoles();

        IEnumerable<IdentityRoleEntity> GetRoles(IEnumerable<Role> roles);

        #endregion
    }
}