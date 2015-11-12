using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangsterBank.DataAccess.Repositories
{
    using System.Diagnostics.Contracts;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Extensions;
    using GangsterBank.Domain.Entities.Clients;

    public class ObligationRepository : BaseRepository<Obligation>, IObligationRepository
    {
            

        #region Public Methods and Operators

        public ObligationRepository(GangsterBankContext context)
            : base(context)
        {

        }

        #endregion

        public IEnumerable<Obligation> GetObligationsForClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());

            IEnumerable<Obligation> obligations =
                this.Context.Clients.ActiveEntities()
                    .Where(client => client.Id == clientId)
                    .SelectMany(client => client.Obligations).ToList().ActiveEntities();
            return obligations;
        }
    }
}
