namespace GangsterBank.Domain.Entities.Clients.TakenLoan
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GangsterBank.Domain.Entities.Base;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Credits;

    public class TakenLoan : BaseEntity
    {
        public TakenLoan()
        {
        }

        public virtual LoanProduct ProductLoan { get; set; }

        public virtual DateTime TakeDate { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual int MaturityInMonth { get; set; }

        public virtual ICollection<LoanPayment> Payments { get; set; }

        public virtual TakenLoanStatus Status { get; set; }

        public virtual Client Client { get; set; }

    }
}
