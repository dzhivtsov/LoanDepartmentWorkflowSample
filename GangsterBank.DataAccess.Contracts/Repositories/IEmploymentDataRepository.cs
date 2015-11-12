namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IEmploymentDataRepository : IRepository<EmploymentData>
    {
        EmploymentData GetClientEmployment(int clientId);
    }
}