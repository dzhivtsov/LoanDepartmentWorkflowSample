namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface ICitiesRepository : IRepository<City>
    {
        IEnumerable<string> GetCityNamesStartingWith(string cityNameStarting);

        City GetByName(string cityName);
    }
}