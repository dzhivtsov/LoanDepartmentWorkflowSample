namespace GangsterBank.Web.Models.Operator
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UnconfirmedClientViewModel
    {
        #region Public Properties

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int ClientId { get; set; }

        public string Employment { get; set; }

        public string Name { get; set; }

        public string Passport { get; set; }

        public string PhoneNumber { get; set; }

        public string RegistrationAddress { get; set; }

        public string ResidentialAddress { get; set; }

        #endregion
    }
}