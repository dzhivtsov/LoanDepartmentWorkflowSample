namespace GangsterBank.Domain.Entities.CreditPlans
{
    public abstract class BaseCreditPlan
    {
        public decimal CreditSum { get; set; }
        public int Percents { get; set; }
        public int CreditPeriodInMonths { get; set; }
    }
}
