namespace GangsterBank.BusinessLogic.Contracts.Credits
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Credits;

    public interface ILoanRequestsService
    {
        #region Public Methods and Operators

        void ApproveLoanRequest(LoanRequest loanRequest, IUserContext userContext);

        void ApproveLoanRequest(int loanRequestId, IUserContext userContext);

        void CreateLoanRequest(Client client, LoanProduct loanProduct, decimal amount, int months);

        void DeclineLoanRequest(int loanRequestId, IUserContext userContext);

        void DeclineLoanRequest(LoanRequest loanRequest, IUserContext userContext);

        IEnumerable<LoanRequest> GetRequestsForApproval(IUserContext userContext);

        #endregion
    }
}