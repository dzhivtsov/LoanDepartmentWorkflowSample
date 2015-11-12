namespace GangsterBank.BusinessLogic.Credits.RequestPrerequisiteRules
{
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.Entities.Credits;

    public class AmountRangeLoanRequestPrerequisiteRule : ILoanRequestPrerequisiteRule
    {
        public static string ErrorConst = "Incorrect amount";

        #region Public Methods and Operators

        string ILoanRequestPrerequisiteRule.IsValid(LoanRequest loanRequest)
        {
            return loanRequest.Amount >= loanRequest.LoanProduct.MinAmount
                && loanRequest.Amount <= loanRequest.LoanProduct.MaxAmount ? string.Empty : ErrorConst;
        }

        public bool IsValid(LoanRequest loanRequest)
        {
            return loanRequest.Amount >= loanRequest.LoanProduct.MinAmount
                   && loanRequest.Amount <= loanRequest.LoanProduct.MaxAmount;
        }

        #endregion
    }
}