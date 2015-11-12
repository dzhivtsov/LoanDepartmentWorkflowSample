namespace GangsterBank.BusinessLogic.Contracts.Clients
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IPersonalDetailsService
    {
        PersonalDetails GetPersonalDetailsForClient(int clientId);
    }
}