namespace GangsterBank.BusinessLogic.Credits.RequestPrerequisiteRules
{
    using System;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.Entities.Credits;

    public class EmploymentTermLoanRequestPrerequisiteRule : ILoanRequestPrerequisiteRule
    {
        public static string Error = "Not enough time on the current work";

        #region Public Methods and Operators

        public string IsValid(LoanRequest loanRequest)
        {
            if (!loanRequest.LoanProduct.Requirements.NeedEarningsRecord)
            {
                return string.Empty;
            }

            return loanRequest.Client.PersonalDetails.EmploymentData.HireDate
                <= DateTime.UtcNow.AddMonths(loanRequest.LoanProduct.Requirements.MinWorkOnLastJobInMonths) ? string.Empty : Error;
        }

        #endregion
    }
}