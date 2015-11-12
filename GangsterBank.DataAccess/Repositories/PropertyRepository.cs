namespace GangsterBank.DataAccess.Repositories
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Extensions;
    using GangsterBank.Domain.Entities.Clients;

    #endregion

    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        #region Constructors and Destructors

        public PropertyRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<Property> GetPropertiesForClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());

            IEnumerable<Property> properties =
                this.Context.Clients.ActiveEntities()
                    .Where(client => client.Id == clientId)
                    .SelectMany(client => client.Properties).ToList().ActiveEntities();
            return properties;
        }

        #endregion
    }
}