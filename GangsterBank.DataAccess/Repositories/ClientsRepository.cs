namespace GangsterBank.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class ClientsRepository : BaseRepository<Client>, IClientsRepository
    {
        #region Constructors and Destructors

        public ClientsRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<Client> GetNotConfirmedClients()
        {
            IQueryable<Client> clients = from client in this.ActiveEntities where !client.IsConfirmed select client;
            return clients;
        }

        #endregion
    }
}