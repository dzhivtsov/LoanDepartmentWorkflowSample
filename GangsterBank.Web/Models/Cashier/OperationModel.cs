namespace GangsterBank.Web.Models.Cashier
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    public class OperationModel
    {
        [Required]
        [Positive]
        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public decimal Amount { get; set; }

        public decimal CurrentAmount { get; set; }
    }
}