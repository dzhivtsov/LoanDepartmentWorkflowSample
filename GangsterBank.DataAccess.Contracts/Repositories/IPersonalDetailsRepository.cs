namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IPersonalDetailsRepository : IRepository<PersonalDetails>
    {
        PersonalDetails GetPersonalDetails(int clientId);
    }
}