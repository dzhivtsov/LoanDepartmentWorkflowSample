namespace GangsterBank.BusinessLogic.Credits.RequestPrerequisiteRules
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Credits;

    public class CompositeLoanRequestPrerequisiteRule : ILoanRequestPrerequisiteRule
    {
        #region Fields

        private readonly IEnumerable<ILoanRequestPrerequisiteRule> rules = new ILoanRequestPrerequisiteRule[]
                                                                           {
                                                                               new AmountRangeLoanRequestPrerequisiteRule(), 
                                                                               new TermRangeLoanRequestPrerequisiteRule(), 
                                                                               new EmploymentTermLoanRequestPrerequisiteRule(), 
                                                                               new SalaryLoanRequestPrerequisiteRule()
                                                                           };

        #endregion

        #region Public Methods and Operators

        public string Error { get; set; }

        public string IsValid(LoanRequest loanRequest)
        {
            Contract.Requires<ArgumentNullException>(loanRequest.IsNotNull());
            var list = this.rules.Select(
                rule =>
                    {
                        var isValid = rule.IsValid(loanRequest);
                        return isValid.ToLower();
                    });
            list = list.Where(x => x != string.Empty);
            return string.Join(", ", list);
        }

        #endregion
    }
}