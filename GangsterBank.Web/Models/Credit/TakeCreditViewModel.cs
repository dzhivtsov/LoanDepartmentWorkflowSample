namespace GangsterBank.Web.Models.Credit
{
    using System.ComponentModel.DataAnnotations;

    public class TakeCreditViewModel: TakeCreditModel
    {
        [Display(Name = "Credit Name")]
        public string CreditProductName { get; set; }

        public int MaxAmount { get; set; }

        public int MinAmount { get; set; }

        public int MaxPeriod { get; set; }

        public int MinPeriod { get; set; }
    }
}