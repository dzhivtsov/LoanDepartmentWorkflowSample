namespace GangsterBank.Domain.Entities.Accounts
{
    using GangsterBank.Domain.Entities.Base;
    using GangsterBank.Domain.Entities.Clients;

    public class Account : BaseEntity
    {
        public virtual decimal Amount { get; set; }
        
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

    }
}