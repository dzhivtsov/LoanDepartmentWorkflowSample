namespace GangsterBank.Domain.BusinessLogicEntities.Statistics
{
    using GangsterBank.Domain.Entities.Credits;

    public class SummaryLoanProductStatistic
    {
        public int LoanProductId { get; set; }

        public string LoanProductName { get; set; }

        public LoanProductStatus Status { get; set; }

        public int TakeCount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
