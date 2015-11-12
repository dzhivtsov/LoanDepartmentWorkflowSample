namespace GangsterBank.Web.Models.ClientProfile
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class ContactsViewModel
    {
        #region Public Properties

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Positive]
        public int ClientId { get; set; }

        [Required]
        [DisplayName("Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Registration address")]
        public AddressViewModel RegistrationAddress { get; set; }

        [Required]
        [DisplayName("Residential address")]
        public AddressViewModel ResidentialAddress { get; set; }

        #endregion
    }
}