namespace GangsterBank.Web.Models.ClientProfile
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foolproof;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;
    using GangsterBank.Web.Infrastructure.Validators;

    public class EmploymentViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        [Positive]
        public int ClientId { get; set; }
        
        [DisplayName("Unemployed")]
        public bool IsUnemployed { get; set; }

        [RequiredIfFalse("IsUnemployed")]
        [DisplayName("Company name")]
        public string Company { get; set; }

        [RequiredIfFalse("IsUnemployed")]
        [DisplayName("Position")]
        public string Position { get; set; }

        [RequiredIfFalse("IsUnemployed")]
        [DisplayName("Salary")]
        [Positive]
        public decimal? Salary { get; set; }

        [RequiredIfFalse("IsUnemployed")]
        [DisplayName("Hire date")]
        [DataType(DataType.Date)]
        [BeforeToday]
        public DateTime? HireDate { get; set; }
    }
}