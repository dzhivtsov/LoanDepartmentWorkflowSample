namespace GangsterBank.BusinessLogic.Contracts.Credits
{
    #region

    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Membership;

    #endregion

    public interface IUserContext
    {
        #region Public Properties

        User User { get; }

        bool IsAdministrator { get; }

        bool IsClient { get; }

        bool IsOperator { get; }

        bool IsLendingDepartmentSpecialist { get; }

        bool IsLendingDepartmentHead { get; }

        bool IsSecuritySpecialist { get; }

        bool IsCashier { get; }

        IEnumerable<Role> Roles { get; }
        IEnumerable<IdentityRoleEntity> IdentityRoleEntities { get; }

        #endregion
    }
}