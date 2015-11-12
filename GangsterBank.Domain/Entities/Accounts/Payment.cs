namespace GangsterBank.Domain.Entities.Accounts
{
    using System;

    using GangsterBank.Domain.Entities.Base;

    public class Payment : BaseEntity
    {
        public virtual Account Account { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual DateTime DateTime { get; set; }

        public virtual PaymentType Type { get; set; }
    }
}