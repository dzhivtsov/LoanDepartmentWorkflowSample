using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IPropertyRepository : IRepository<Property>
    {
        IEnumerable<Property> GetPropertiesForClient(int clientId);
    }
}
