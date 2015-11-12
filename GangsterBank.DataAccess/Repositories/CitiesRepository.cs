namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Security.Cryptography;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class CitiesRepository : BaseRepository<City>, ICitiesRepository
    {
        #region Constructors and Destructors

        public CitiesRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<string> GetCityNamesStartingWith(string cityNameStarting)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(cityNameStarting));

            IQueryable<string> cityNames = from city in this.DbSet
                                           where city.Name.StartsWith(cityNameStarting)
                                           select city.Name;
            return cityNames;
        }

        public City GetByName(string cityName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(cityName));
            City existingCity = this.DbSet.FirstOrDefault(city => city.Name.ToLower() == cityName.Trim().ToLower());
            this.ThrowNotFoundException(existingCity);
            return existingCity;
        }

        #endregion
    }
}