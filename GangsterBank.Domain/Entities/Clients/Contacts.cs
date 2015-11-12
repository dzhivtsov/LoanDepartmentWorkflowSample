namespace GangsterBank.Domain.Entities.Clients
{
    using GangsterBank.Domain.Entities.Base;

    public class Contacts : BaseEntity
    {
        public virtual string PhoneNumber { get; set; }

        public virtual Address RegistrationAddress { get; set; }

        public virtual Address ResidentialAddress { get; set; }
    }
}