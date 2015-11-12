namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface ICountriesRepository : IRepository<Country>
    {
        #region Public Methods and Operators

        IEnumerable<string> GetCountryNamesStartingWith(string countryNameStarting);

        #endregion

        Country GetByName(string countryName);
    }
}