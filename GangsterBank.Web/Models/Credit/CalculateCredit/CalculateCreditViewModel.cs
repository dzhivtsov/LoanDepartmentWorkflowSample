namespace GangsterBank.Web.Models.Credit.CalculateCredit
{
    public class CalculateCreditViewModel : CalculateCreditModel
    {
        public int MinPeriod { get; set; }

        public int MaxPeriod { get; set; }

        public double MinAmount { get; set; }

        public double MaxAmount { get; set; }

        public decimal MinAmountD { get; set; }

        public decimal MaxAmountD { get; set; }
    }
}