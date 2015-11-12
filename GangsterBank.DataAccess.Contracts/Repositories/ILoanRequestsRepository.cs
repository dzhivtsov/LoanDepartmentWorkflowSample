namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.Membership;

    public interface ILoanRequestsRepository : IRepository<LoanRequest>
    {
        #region Public Methods and Operators

        IEnumerable<LoanRequest> GetLoanRequestsByStatus(LoanRequestStatus status);

        IEnumerable<LoanRequest> GetRequestsByStatusAndRemainingApprovers(
            LoanRequestStatus loanRequestStatus,
            IEnumerable<IdentityRoleEntity> identityRoles);

        #endregion
    }
}