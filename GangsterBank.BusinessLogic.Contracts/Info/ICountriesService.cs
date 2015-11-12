namespace GangsterBank.BusinessLogic.Contracts.Info
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface ICountriesService
    {
        IEnumerable<string> SearchCountryNames(string countryName);

        Country GetCountryByName(string countryName);
    }
}