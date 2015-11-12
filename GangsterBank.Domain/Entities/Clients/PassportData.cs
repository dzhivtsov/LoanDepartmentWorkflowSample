namespace GangsterBank.Domain.Entities.Clients
{
    using System;

    using GangsterBank.Domain.Entities.Base;

    public class PassportData : BaseEntity
    {
        public virtual byte[] DigitalCopy { get; set; }

        public virtual string PersonalNumber { get; set; }

        public virtual string PassportNumber { get; set; }

        public virtual string Issuer { get; set; }

        public virtual DateTime IssueDate { get; set; }

        public virtual DateTime ExpirationDate { get; set; }
    }
}