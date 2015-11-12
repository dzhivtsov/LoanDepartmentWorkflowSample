namespace GangsterBank.BusinessLogic.Contracts.Credits
{
    using GangsterBank.Domain.Entities.Credits;

    public interface ILoanRequestPrerequisiteRule
    {
        string IsValid(LoanRequest loanRequest);
    }
}