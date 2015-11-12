namespace GangsterBank.BusinessLogic.Clients
{
    using System;
    using System.Diagnostics.Contracts;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients;

    public class ContactsService : IContactsService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        #endregion

        #region Constructors and Destructors

        public ContactsService(IGangsterBankUnitOfWork gangsterBankUnitOfWork)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork.IsNotNull());
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        #endregion

        #region Public Methods and Operators

        public Contacts GetContactsForClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            Contacts contacts = this.gangsterBankUnitOfWork.ContactsRepository.GetContacts(clientId);
            return contacts;
        }

        #endregion
    }
}