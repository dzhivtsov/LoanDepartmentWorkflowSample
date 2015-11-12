namespace GangsterBank.SchedulerService
{
    using System.Collections.Generic;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Membership;

    public class UserContext : IUserContext
    {
        #region Public Properties

        public IEnumerable<IdentityRoleEntity> IdentityRoleEntities { get; private set; }

        public bool IsAdministrator { get; private set; }

        public bool IsClient { get; private set; }

        public bool IsLendingDepartmentHead { get; private set; }

        public bool IsLendingDepartmentSpecialist { get; private set; }

        public bool IsOperator { get; private set; }

        public bool IsSecuritySpecialist { get; private set; }
        public bool IsCashier { get; private set; }

        public IEnumerable<Role> Roles { get; private set; }

        public User User
        {
            get
            {
                return new Client();
            }
        }

        #endregion
    }
}