using System.Web.Mvc;

namespace GangsterBank.Web.Models.Credit
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class TakeCreditModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        [Positive]
        public int CreditProductId { get; set; }
        
        [Required]
        [Positive]
        [DisplayName("Amount")]
        public decimal Sum { get; set; }

        [Required]
        [Positive]
        [DisplayName("Period in month")]
        public int PeriodInMonth { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        [Positive]
        public int ClientId { get; set; }
    }
}