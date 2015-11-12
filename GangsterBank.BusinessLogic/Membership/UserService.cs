namespace GangsterBank.BusinessLogic.Membership
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using GangsterBank.BusinessLogic.Contracts.Membership;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity;

    #endregion

    public class UserService : UserManager<User, int>, IUserService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        #endregion

        #region Constructors and Destructors

        public UserService(IUserEntityStore store, IGangsterBankUnitOfWork gangsterBankUnitOfWork)
            : base(store)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork.IsNotNull());
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<IdentityRoleEntity> GetRoles()
        {
            return this.gangsterBankUnitOfWork.RolesRepository.GetRoles();
        }

        public IEnumerable<IdentityRoleEntity> GetRoles(IEnumerable<Role> roles)
        {
            Contract.Requires<ArgumentNullException>(roles.IsNotNull());
            IEnumerable<IdentityRoleEntity> identityRoles = this.gangsterBankUnitOfWork.RolesRepository.GetRoles(roles);
            return identityRoles;
        }

        #endregion
    }
}