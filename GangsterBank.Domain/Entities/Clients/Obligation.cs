namespace GangsterBank.Domain.Entities.Clients
{
    using System;

    using GangsterBank.Domain.Entities.Base;

    public class Obligation : BaseEntity
    {
        public virtual string CompanyName { get; set; }

        public virtual decimal OutstandingAmount { get; set; }

        public virtual decimal? MontlyPayments { get; set; }

        public virtual DateTime ExpirationDate { get; set; }

        public virtual decimal DelayAmount { get; set; }
    }
}