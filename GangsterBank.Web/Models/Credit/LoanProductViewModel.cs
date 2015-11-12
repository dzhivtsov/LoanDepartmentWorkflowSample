namespace GangsterBank.Web.Models.Credit
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foolproof;

    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class LoanProductViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int LoanProductId { get; set; }

        [Required]
        [Positive]
        [DisplayName("Minimum amount")]
        public decimal MinAmount { get; set; }

        [Required]
        [Positive]
        [DisplayName("Maximum amount")]
        [GreaterThanOrEqualTo("MinAmount")]
        public decimal MaxAmount { get; set; }

        [Required]
        [Positive]
        [DisplayName("Maturity")]
        public int Percentage { get; set; }

        [Required]
        [Positive]
        [DisplayName("Minimum period in months")]
        public int MinPeriodInMonth { get; set; }

        [Required]
        [Positive]
        [DisplayName("Maximum period in months")]
        [GreaterThanOrEqualTo("MinPeriodInMonth")]
        public int MaxPeriodInMonth { get; set; }

        [Required]
        [MinLength(6)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        public LoanProductRequirementsViewModel Requirements { get; set; }

        [Required]
        public LoanProductType Type { get; set; }

        [Required]
        [Range(0, 100)]
        [DisplayName("Fine percentage per day")]
        public int FineDayPercentage { get; set; }

        [Required]
        [Range(0, 100)]
        [DisplayName("Advanced repayment fine percentage")]
        public int AdvancedRepaymentFinePercentage { get; set; }

        [Required]
        [Range(0, 100)]
        [DisplayName("Advanced repayment first possible month")]
        public int AdvancedRepaymentFirstPossibleMonth { get; set; }
    }
}