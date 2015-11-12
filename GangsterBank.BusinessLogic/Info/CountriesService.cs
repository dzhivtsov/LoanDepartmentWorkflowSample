namespace GangsterBank.BusinessLogic.Info
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;

    using GangsterBank.BusinessLogic.Contracts.Info;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Exceptions;

    public class CountriesService : ICountriesService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        #endregion

        #region Constructors and Destructors

        public CountriesService(IGangsterBankUnitOfWork gangsterBankUnitOfWork)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork != null);
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<string> SearchCountryNames(string countryName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(countryName));

            IEnumerable<string> countryNames =
                this.gangsterBankUnitOfWork.CountryRepository.GetCountryNamesStartingWith(countryName);
            return countryNames;
        }

        public Country GetCountryByName(string countryName)
        {
            Contract.Requires<ArgumentNullException>(!countryName.IsNullOrWhiteSpace());

            countryName = countryName.Trim();
            Country country;
            try
            {
                country = this.gangsterBankUnitOfWork.CountryRepository.GetByName(countryName);
            }
            catch (NotFoundException)
            {
                country = new Country { Name = countryName, Cities =  new Collection<City>()};
                this.gangsterBankUnitOfWork.CountryRepository.CreateOrUpdate(country);
            }

            return country;
        }

        #endregion
    }
}