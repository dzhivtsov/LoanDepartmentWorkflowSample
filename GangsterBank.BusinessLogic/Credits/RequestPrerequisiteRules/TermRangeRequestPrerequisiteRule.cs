namespace GangsterBank.BusinessLogic.Credits.RequestPrerequisiteRules
{
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.Entities.Credits;

    public class TermRangeLoanRequestPrerequisiteRule : ILoanRequestPrerequisiteRule
    {
        public static string Error = "Incorrect period";

        #region Public Methods and Operators

        public string IsValid(LoanRequest loanRequest)
        {
            return loanRequest.Months >= loanRequest.LoanProduct.MinPeriodInMonth
                && loanRequest.Months <= loanRequest.LoanProduct.MaxPeriodInMonth ? string.Empty : Error;
        }

        #endregion
    }
}