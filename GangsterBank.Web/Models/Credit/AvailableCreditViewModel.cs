namespace GangsterBank.Web.Models.Credit
{
    public class AvailableCreditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal MinAmount { get; set; }

        public decimal MaxAmount { get; set; }

        public int MinPeriodInMonth { get; set; }

        public int MaxPeriodInMonth { get; set; }

        public int Percentage { get; set; }

        public string Description { get; set; }
    }
}