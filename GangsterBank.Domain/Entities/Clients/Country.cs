namespace GangsterBank.Domain.Entities.Clients
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Base;

    public class Country : BaseEntity
    {
        public virtual ICollection<City> Cities { get; set; }

        public virtual string Name { get; set; }
    }
}