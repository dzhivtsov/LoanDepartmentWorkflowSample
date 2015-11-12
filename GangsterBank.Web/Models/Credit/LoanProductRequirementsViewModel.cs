namespace GangsterBank.Web.Models.Credit
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using GangsterBank.Domain.Entities.Membership;

    public class LoanProductRequirementsViewModel
    {
        [DisplayName("Minimum monthes on current job")]
        [Range(0, 100)]
        public int MinWorkOnLastJobInMonths { get; set; }

        [DisplayName("Minimum salary")]
        [Range(0, int.MaxValue)]
        public decimal MinSalary { get; set; }

        [DisplayName("Employment is required")]
        public bool NeedEarningsRecord { get; set; }

        [DisplayName("Guarantors are required")]
        public bool NeedGuarantors { get; set; }

        [DisplayName("Guarantors count")]
        public int GuarantorsCount { get; set; }

        public IEnumerable<Role> Approvers { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Range(0, int.MaxValue)]
        public int LoanProductRequirementsId { get; set; }

        public LoanProductRequirementsViewModel()
        {
            this.Approvers = new List<Role>();
        }
    }
}