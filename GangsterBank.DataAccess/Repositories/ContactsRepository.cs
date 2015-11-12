namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class ContactsRepository : BaseRepository<Contacts>, IContactsRepository
    {
        #region Constructors and Destructors

        public ContactsRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public Contacts GetContacts(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            Contacts contacts =
                (from client in this.Context.Clients 
                 where client.Id == clientId 
                 select client.PersonalDetails.Contacts)
                    .SingleOrDefault();
            this.ThrowNotFoundException(contacts);
            return contacts;
        }

        #endregion
    }
}