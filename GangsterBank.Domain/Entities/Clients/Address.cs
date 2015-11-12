namespace GangsterBank.Domain.Entities.Clients
{
    using GangsterBank.Domain.Entities.Base;

    public class Address : BaseEntity
    {
        public virtual Country Country { get; set; }

        public virtual City City { get; set; }

        public virtual string PostIndex { get; set; }

        public virtual string Street { get; set; }

        public virtual int HouseNumber { get; set; }

        public virtual int? CaseNumber { get; set; }

        public virtual int FlatNumber { get; set; }
    }
}