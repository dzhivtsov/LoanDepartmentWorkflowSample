using System;

namespace GangsterBank.Web.Models
{
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Web.Infrastructure.Extensions;

    public class ClientCreditModel
    {
        public string CreditPlanName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime NextPaymentDate { get; set; }

        public string NextPaymentAmout { get; set; }

        public string Description { get; set; }

        public ClientCreditModel(TakenLoan takenLoan)
        {
            Contract.Requires<NullReferenceException>(takenLoan.IsNotNull());
            this.CreditPlanName = takenLoan.ProductLoan.Name;
            this.ExpirationDate = takenLoan.TakeDate.AddMonths(takenLoan.MaturityInMonth);
            if (takenLoan.Payments.Any())
            {
                var nextPayment = takenLoan.Payments.OrderBy(x => x.Date).First(x => x.Date > DateTime.Today);
                this.NextPaymentDate = nextPayment.Date;
                this.NextPaymentAmout = nextPayment.Amount.ToGBString();
            }
            this.Description = takenLoan.ProductLoan.Description;
        }
    }
}