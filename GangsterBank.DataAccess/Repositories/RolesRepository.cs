namespace GangsterBank.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Membership;

    public class RolesRepository : BaseRepository<IdentityRoleEntity>, IRolesRepository
    {
        #region Constructors and Destructors

        public RolesRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<IdentityRoleEntity> GetRoles()
        {
            return this.DbSet;
        }

        public IEnumerable<IdentityRoleEntity> GetRoles(IEnumerable<Role> roles)
        {
            IEnumerable<string> roleNames = roles.Select(role => role.ToString());
            IQueryable<IdentityRoleEntity> identityRoles = from identityRole in this.Context.Roles
                                                           where roleNames.Contains(identityRole.Name)
                                                           select identityRole;
            return identityRoles;
        }

        #endregion
    }
}