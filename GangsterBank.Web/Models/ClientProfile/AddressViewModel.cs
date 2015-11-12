namespace GangsterBank.Web.Models.ClientProfile
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using GangsterBank.Web.Controllers;
    using GangsterBank.Web.Infrastructure.MetadataAttributes;
    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class AddressViewModel
    {
        #region Public Properties

        [DisplayName("Country")]
        [Required]
        [AutoComplete(typeof(GeoInfoController), "Countries")]
        public string Country { get; set; }

        [DisplayName("City")]
        [Required]
        [AutoComplete(typeof(GeoInfoController), "Cities")]
        public string City { get; set; }

        [DisplayName("Postal code")]
        [DataType(DataType.PostalCode)]
        public string PostIndex { get; set; }

        [DisplayName("Street")]
        [Required]
        public string Street { get; set; }

        [DisplayName("House number")]
        [Required]
        [Positive]
        public int? HouseNumber { get; set; }

        [DisplayName("Case number")]
        [Range(1, 100)]
        public int? CaseNumber { get; set; }

        [DisplayName("Flat number")]
        [Required]
        [Positive]
        public int FlatNumber { get; set; }

        #endregion
    }
}