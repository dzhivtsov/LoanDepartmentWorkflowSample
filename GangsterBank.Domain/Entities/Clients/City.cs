namespace GangsterBank.Domain.Entities.Clients
{
    using GangsterBank.Domain.Entities.Base;

    public class City : BaseEntity
    {
        public virtual string Name { get; set; }
    }
}