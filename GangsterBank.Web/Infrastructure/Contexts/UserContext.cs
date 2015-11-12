namespace GangsterBank.Web.Infrastructure.Contexts
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.Membership;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.Helpers.Membership;

    using Microsoft.Ajax.Utilities;

    #endregion

    public class UserContext : IUserContext
    {
        #region Fields

        private readonly Lazy<User> user;

        private readonly IUserService userService;

        #endregion

        #region Constructors and Destructors

        public UserContext(ICurrentUserRetriever currentUserRetriever, IUserService userService)
        {
            Contract.Requires<ArgumentNullException>(currentUserRetriever.IsNotNull());
            Contract.Requires<ArgumentNullException>(userService.IsNotNull());
            this.user = new Lazy<User>(currentUserRetriever.GetCurrentUser);
            this.userService = userService;
        }

        #endregion

        #region Public Properties

        public IEnumerable<IdentityRoleEntity> IdentityRoleEntities
        {
            get
            {
                IEnumerable<IdentityRoleEntity> identityRoleEntities = this.userService.GetRoles(this.Roles).ToList();
                return identityRoleEntities;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return this.IsInRole(Role.Administrator);
            }
        }

        public bool IsClient
        {
            get
            {
                return this.IsInRole(Role.Client);
            }
        }

        public bool IsLendingDepartmentHead
        {
            get
            {
                return this.IsInRole(Role.LendingDepartmentHead);
            }
        }

        public bool IsLendingDepartmentSpecialist
        {
            get
            {
                return this.IsInRole(Role.LendingDepartmentSpecialist);
            }
        }

        public bool IsOperator
        {
            get
            {
                return this.IsInRole(Role.Operator);
            }
        }

        public bool IsSecuritySpecialist
        {
            get
            {
                return this.IsInRole(Role.SecuritySpecialist);
            }
        }

        public bool IsCashier
        {
            get
            {
                return this.IsInRole(Role.Cashier);
            }
        }

        public IEnumerable<Role> Roles
        {
            get
            {
                IList<string> rolesNames = this.userService.GetRolesAsync(this.user.Value.Id).Result;
                IEnumerable<Role> roles = rolesNames.Select(roleName => Enum.Parse(typeof(Role), roleName)).Cast<Role>();
                return roles;
            }
        }

        public User User
        {
            get
            {
                return this.user.Value;
            }
        }

        #endregion

        #region Methods

        private bool IsInRole(Role role)
        {
            return this.userService.IsInRoleAsync(this.user.Value.Id, role.ToString()).Result;
        }

        #endregion
    }
}