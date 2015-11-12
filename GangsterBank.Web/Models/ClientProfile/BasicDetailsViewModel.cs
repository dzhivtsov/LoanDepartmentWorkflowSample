namespace GangsterBank.Web.Models.ClientProfile
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Web.Infrastructure.ValidatonAttributes;
    using GangsterBank.Web.Infrastructure.Validators;

    public class BasicDetailsViewModel
    {
        [Required]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Birth date")]
        [DataType(DataType.Date)]
        [BeforeToday]
        public DateTime? BirthDate { get; set; }

        [Required]
        [DisplayName("Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Positive]
        public int ClientId { get; set; }
    }
}