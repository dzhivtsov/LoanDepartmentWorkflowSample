namespace GangsterBank.Domain.Entities.Clients
{
    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Membership;

    public class Client : User
    {
        public virtual PersonalDetails PersonalDetails { get; set; }

        public virtual ICollection<Obligation> Obligations { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<TakenLoan.TakenLoan> TakenLoans { get; set; }

        public Account PrimaryAccount
        {
            get
            {
                Account account = this.Accounts.FirstOrDefault();
                if (account == null)
                {
                    account = new Account();
                    this.Accounts.Add(account);
                }

                return account;
            }
        } 
    }
}