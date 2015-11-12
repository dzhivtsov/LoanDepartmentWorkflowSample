namespace GangsterBank.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.Membership;

    public class LoanRequestsRepository : BaseRepository<LoanRequest>, ILoanRequestsRepository
    {
        #region Constructors and Destructors

        public LoanRequestsRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<LoanRequest> GetLoanRequestsByStatus(LoanRequestStatus status)
        {
            IQueryable<LoanRequest> loanRequests = from loanRequest in this.ActiveEntities
                                                   where loanRequest.Status == status
                                                   select loanRequest;
            return loanRequests;
        }

        public IEnumerable<LoanRequest> GetRequestsByStatusAndRemainingApprovers(
            LoanRequestStatus loanRequestStatus, 
            IEnumerable<IdentityRoleEntity> identityRoles)
        {
            IEnumerable<LoanRequest> loanRequests =
                (from loanRequest in this.ActiveEntities
                 where loanRequest.Status == loanRequestStatus
                 select loanRequest).ToList();
            loanRequests =
                loanRequests.Where(loanRequest => loanRequest.RemainingApprovers.Intersect(identityRoles).Any());
            return loanRequests;
        }

        #endregion
    }
}