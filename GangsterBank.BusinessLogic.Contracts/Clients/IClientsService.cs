namespace GangsterBank.BusinessLogic.Contracts.Clients
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface IClientsService
    {
        Client GetClient(int clientId);

        IEnumerable<Client> GetClientsForApproval();

        void ConfirmClient(Client client);

        IEnumerable<Client> GetAllClients();

        IEnumerable<Client> GetAllConfirmedClients();

        void CreateOrUpdate(Client client);
    }
}