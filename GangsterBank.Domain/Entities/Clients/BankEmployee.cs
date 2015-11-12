namespace GangsterBank.Domain.Entities.Clients
{
    using System;

    using GangsterBank.Domain.Entities.Membership;

    public class BankEmployee : User
    {
        public virtual DateTime EmploymentDate { get; set; }
    }
}