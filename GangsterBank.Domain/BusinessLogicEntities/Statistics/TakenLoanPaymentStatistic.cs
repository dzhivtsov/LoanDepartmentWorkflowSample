namespace GangsterBank.Domain.BusinessLogicEntities.Statistics
{
    public class TakenLoanPaymentStatistic
    {
        public string PaymentCategory { get; set; }

        public int TakeCount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
