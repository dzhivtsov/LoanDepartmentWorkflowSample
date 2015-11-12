namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IPassportDataRepository : IRepository<PassportData>
    {
        PassportData GetPassportDataForClient(int clientId);
    }
}