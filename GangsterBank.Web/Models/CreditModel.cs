namespace GangsterBank.Web.Models
{
    public class CreditModel
    {
        public string Name { get; set; }

        public string Percents { get; set; }

        public decimal MinAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public string MinPeriod { get; set; }

        public string MaxPeriod { get; set; }

        public string Comments { get; set; }
    }
}