namespace GangsterBank.Web.Models.ClientProfile
{
    #region

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Foolproof;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    #endregion

    public class PassportViewModel
    {
        #region Public Properties

        [Required]
        [Positive]
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }
        
        [Required]
        [DisplayName("Passport number")]
        [RegularExpression(@"(AB|BM|HB|KH|MP|MH|MC|KB|PP|АВ|ВМ|НВ|КН|МР|МН|МС|КВ|РР)\d{7}",
            ErrorMessage = "Invalid passport number")]
        public string PassportNumber { get; set; }

        [Required]
        [DisplayName("Personal number")]
        [RegularExpression(@"\d{7}[ABCHKEMabchkemАВСНКЕМавснкем]\d{3}(PB|РВ)\d",
            ErrorMessage = "Invalid personal number")]
        public string PersonalNumber { get; set; }

        [Required]
        [DisplayName("Issue date")]
        [DataType(DataType.Date)]
        [BeforeToday]
        public DateTime? IssueDate { get; set; }

        [Required]
        [DisplayName("Issuer")]
        public string Issuer { get; set; }

        [Required]
        [DisplayName("Expiration date")]
        [DataType(DataType.Date)]
        [GreaterThan("IssueDate")]
        public DateTime? ExpirationDate { get; set; }
        
        [RequiredIfFalse("HasImage", ErrorMessage = "Passport scan is required")]
        public HttpPostedFileBase File { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool HasImage { get; set; }
        #endregion
    }
}