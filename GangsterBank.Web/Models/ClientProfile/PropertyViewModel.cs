namespace GangsterBank.Web.Models.ClientProfile
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Web.WebPages.Instrumentation;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class PropertyViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Cost")]
        [Positive]
        public decimal Cost { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PropertyId { get; set; }
    }
}