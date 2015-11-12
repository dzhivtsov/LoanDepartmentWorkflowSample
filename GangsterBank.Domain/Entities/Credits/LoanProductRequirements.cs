namespace GangsterBank.Domain.Entities.Credits
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Base;
    using GangsterBank.Domain.Entities.Membership;

    public class LoanProductRequirements : BaseEntity
    {
        public virtual int MinWorkOnLastJobInMonths { get; set; }

        public virtual decimal MinSalary { get; set; }

        public virtual bool NeedEarningsRecord { get; set; }

        public virtual bool NeedGuarantors { get; set; }

        public virtual int GuarantorsCount { get; set; }

        public virtual ICollection<IdentityRoleEntity> Approvers { get; set; }

        public LoanProductRequirements()
        {
            
        }

        public LoanProductRequirements(LoanProductRequirements requirements)
        {
            this.Update(requirements);
        }

        public void Update(LoanProductRequirements requirements)
        {
            Contract.Requires<ArgumentNullException>(requirements.IsNotNull());
            this.MinWorkOnLastJobInMonths = requirements.MinWorkOnLastJobInMonths;
            this.MinSalary = requirements.MinSalary;
            this.NeedEarningsRecord = requirements.NeedEarningsRecord;
            this.NeedGuarantors = requirements.NeedGuarantors;
            this.GuarantorsCount = requirements.GuarantorsCount;
            var tempApprovers = requirements.Approvers.ToList();
            this.Approvers.Clear();
            foreach (var approver in tempApprovers)
            {
                Approvers.Add(approver);
            }
        }
    }
}
