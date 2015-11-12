namespace GangsterBank.Web.Models.Credit
{
    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Credits;

    public class CreateCreditModel
    {
        public LoanProduct LoanProduct { get; set; }
    }
}