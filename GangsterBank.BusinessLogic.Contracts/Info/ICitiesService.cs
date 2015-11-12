namespace GangsterBank.BusinessLogic.Contracts.Info
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface ICitiesService
    {
        IEnumerable<string> SearchCityNames(string cityName);

        City GetCityByName(string cityName);
    }
}