namespace GangsterBank.Web.Models.Credit.CalculateCredit
{
    using GangsterBank.Domain.Entities.Credits;

    public class CalculateCreditModel
    {
        public int LoanProductId { get; set; }

        public int Monthes { get; set; }

        public decimal Amount { get; set; }

        public LoanProductType LoanProductType { get; set; }
    }
}