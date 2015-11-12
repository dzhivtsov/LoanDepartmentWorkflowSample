namespace GangsterBank.BusinessLogic.Info
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using GangsterBank.BusinessLogic.Contracts.Info;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Exceptions;

    public class CitiesService : ICitiesService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        #endregion

        #region Constructors and Destructors

        public CitiesService(IGangsterBankUnitOfWork gangsterBankUnitOfWork)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork != null);
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        #endregion

        #region Public Methods and Operators

        public City GetCityByName(string cityName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(cityName));

            cityName = cityName.Trim();
            City city;
            try
            {
                city = this.gangsterBankUnitOfWork.CitiesRepository.GetByName(cityName);
            }
            catch (NotFoundException)
            {
                city = new City { Name = cityName };
                this.gangsterBankUnitOfWork.CitiesRepository.CreateOrUpdate(city);
            }

            return city;
        }

        public IEnumerable<string> SearchCityNames(string cityName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(cityName));

            IEnumerable<string> cityNames =
                this.gangsterBankUnitOfWork.CitiesRepository.GetCityNamesStartingWith(cityName);
            return cityNames;
        }

        #endregion
    }
}