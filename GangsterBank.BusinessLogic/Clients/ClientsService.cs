namespace GangsterBank.BusinessLogic.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.EmailTemplates;

    public class ClientsService : IClientsService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        private readonly IMailService mailService;

        #endregion

        #region Constructors and Destructors

        public ClientsService(
            IGangsterBankUnitOfWork gangsterBankUnitOfWork,
            IMailService mailService)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork != null);
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
            this.mailService = mailService;
        }

        #endregion

        #region Public Methods and Operators

        public Client GetClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            Client client = this.gangsterBankUnitOfWork.ClientsRepository.GetById(clientId);
            return client;
        }

        public IEnumerable<Client> GetClientsForApproval()
        {
            IEnumerable<Client> clients = this.gangsterBankUnitOfWork.ClientsRepository.GetNotConfirmedClients();
            return clients;
        }

        public void ConfirmClient(Client client)
        {
            Contract.Requires<ArgumentNullException>(client.IsNotNull());
            client.IsConfirmed = true;
            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(client);
            this.mailService.SendMessage(EmailTemplateType.ApproveUser, client);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return this.gangsterBankUnitOfWork.ClientsRepository.GetAll();
        }

        public IEnumerable<Client> GetAllConfirmedClients()
        {
            return this.gangsterBankUnitOfWork.ClientsRepository.GetAll().Where(x => x.IsConfirmed);
        }

        public void CreateOrUpdate(Client client)
        {
            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(client);
        }

        #endregion
    }
}