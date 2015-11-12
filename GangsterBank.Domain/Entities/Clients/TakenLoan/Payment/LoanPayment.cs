namespace GangsterBank.Domain.Entities.Clients.TakenLoan.Payment
{
    using System;

    using GangsterBank.Domain.Entities.Base;

    public class LoanPayment : BaseEntity
    {
        public virtual DateTime Date { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual decimal Fine { get; set; }

        public virtual LoanPaymentStatus Status { get; set; }

        public virtual TakenLoan TakenLoan { get; set; }
    }
}
