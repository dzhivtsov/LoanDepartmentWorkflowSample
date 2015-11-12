namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class CountriesRepository : BaseRepository<Country>, ICountriesRepository
    {
        #region Constructors and Destructors

        public CountriesRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public Country GetByName(string countryName)
        {
            Contract.Requires<ArgumentNullException>(!countryName.IsNullOrWhiteSpace());
            Country existingCountry =
                this.DbSet.SingleOrDefault(country => country.Name.ToLower() == countryName.Trim().ToLower());
            this.ThrowNotFoundException(existingCountry);
            return existingCountry;
        }

        public IEnumerable<string> GetCountryNamesStartingWith(string countryNameStarting)
        {
            Contract.Requires<ArgumentNullException>(!countryNameStarting.IsNullOrEmpty());

            IQueryable<string> cityNames = from country in this.DbSet
                                           where country.Name.StartsWith(countryNameStarting)
                                           select country.Name;
            return cityNames;
        }

        #endregion
    }
}