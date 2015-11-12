namespace GangsterBank.Web.Models.Statistics
{
    public class SummaryLoanProductStatisticViewModel : BaseStatisticViewModel
    {
        public int LoanProductId { get; set; }

        public string Status { get; set; }

        public int TakeCount { get; set; }

        public string TotalAmount { get; set; }

    }
}