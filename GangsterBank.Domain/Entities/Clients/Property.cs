namespace GangsterBank.Domain.Entities.Clients
{
    using GangsterBank.Domain.Entities.Base;

    public class Property : BaseEntity
    {
        public virtual string Description { get; set; }

        public virtual decimal Cost { get; set; }
    }
}