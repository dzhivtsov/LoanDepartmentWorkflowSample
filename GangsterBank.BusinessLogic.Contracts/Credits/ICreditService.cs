using System.Collections.Generic;

namespace GangsterBank.BusinessLogic.Contracts.Credits
{
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Credits;

    public interface ICreditService
    {
        void ChangeLoanProductStatus(LoanProduct loanProduct, LoanProductStatus newStatus);

        void ChangeLoanProductStatus(int loanProductId, LoanProductStatus status);

        bool CreateLoanProduct(LoanProduct loanProduct);

        IEnumerable<LoanProduct> GetActiveLoanProducts();

        IEnumerable<LoanProduct> GetArchivedLoanProducts();

        IEnumerable<LoanProduct> GetAvailableLoanProducts();

        /// <summary>
        /// This method removes circular to prevent error in javascript serializer
        /// </summary>
        /// <returns></returns>
        IEnumerable<LoanProduct> GetAvailableLoanProductsForShow();

        IEnumerable<LoanProduct> GetDraftLoanProducts();

        LoanProduct GetLoanProduct(int id);

        IEnumerable<LoanProduct> GetReadyForReviewLoanProducts();

        bool Remove(int loanProductId);

        void Save(LoanProduct loanProduct);

        IEnumerable<TakenLoan> GetAllTakenLoans();

        IEnumerable<LoanProduct> GetAlLoanProducts();
    }
}
