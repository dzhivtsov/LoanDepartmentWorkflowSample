namespace GangsterBank.Domain.Entities.Credits
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Base;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Membership;

    public class LoanRequest : BaseEntity
    {
        #region Public Properties

        public virtual decimal Amount { get; set; }

        public virtual ICollection<IdentityRoleEntity> ApprovedBy { get; set; }

        public virtual Client Client { get; set; }

        public virtual LoanProduct LoanProduct { get; set; }

        public virtual int Months { get; set; }

        public virtual LoanRequestStatus Status { get; set; }

        public IEnumerable<IdentityRoleEntity> RemainingApprovers
        {
            get
            {
                return this.LoanProduct.Requirements.Approvers.Except(this.ApprovedBy);
            }
        }

        #endregion

        #region Public Methods and Operators

        public static LoanRequest Create(
            Client client, 
            LoanProduct loanProduct,  
            decimal amount, 
            int monthes)
        {
            Contract.Requires<ArgumentNullException>(client.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanProduct.IsNotNull());
            Contract.Requires<ArgumentOutOfRangeException>(amount > 0);
            Contract.Requires<ArgumentOutOfRangeException>(monthes > 0);
            
            var loanRequest = new LoanRequest
                                  {
                                      Client = client, 
                                      LoanProduct = loanProduct, 
                                      Status = LoanRequestStatus.Requested, 
                                      Amount = amount, 
                                      Months = monthes,
                                      ApprovedBy = new List<IdentityRoleEntity>()
                                  };
            return loanRequest;
        }

        #endregion
    }
}