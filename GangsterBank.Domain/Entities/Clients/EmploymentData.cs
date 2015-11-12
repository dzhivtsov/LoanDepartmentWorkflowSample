namespace GangsterBank.Domain.Entities.Clients
{
    using System;

    using GangsterBank.Domain.Entities.Base;

    public class EmploymentData : BaseEntity
    {
        public virtual string Company { get; set; }

        public virtual string Position { get; set; }

        public virtual DateTime? HireDate { get; set; }

        public virtual decimal? Salary { get; set; }

        public virtual bool IsUnemployed { get; set; }
    }
}