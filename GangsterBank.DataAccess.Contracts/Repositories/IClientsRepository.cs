namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface IClientsRepository : IRepository<Client>
    {
        IEnumerable<Client> GetNotConfirmedClients();
    }
}