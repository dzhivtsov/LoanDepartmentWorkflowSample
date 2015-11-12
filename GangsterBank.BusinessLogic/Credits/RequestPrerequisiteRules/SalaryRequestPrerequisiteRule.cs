namespace GangsterBank.BusinessLogic.Credits.RequestPrerequisiteRules
{
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.Entities.Credits;

    public class SalaryLoanRequestPrerequisiteRule : ILoanRequestPrerequisiteRule
    {
        public static string Error = "Not enough salary";

        #region Public Methods and Operators

        public string IsValid(LoanRequest loanRequest)
        {
            if (!loanRequest.LoanProduct.Requirements.NeedEarningsRecord)
            {
                return string.Empty;
            }

            return loanRequest.Client.PersonalDetails.EmploymentData.Salary
                >= loanRequest.LoanProduct.Requirements.MinSalary ? string.Empty : Error;
        }

        #endregion
    }
}