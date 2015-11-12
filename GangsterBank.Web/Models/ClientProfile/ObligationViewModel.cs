namespace GangsterBank.Web.Models.ClientProfile
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class ObligationViewModel
    {
        [Required]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

        [Required]
        [Positive]
        [DisplayName("Outstanding amount")]
        public decimal OutstandingAmount { get; set; }

        [Required]
        [Positive]
        [DisplayName("Monthly payment")]
        public decimal MonthlyPayments { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Expiration date")]
        public DateTime? ExpirationDate { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [DisplayName("Delay amount")]
        public decimal DelayAmount { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ObligationId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }
    }
}