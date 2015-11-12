namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IContactsRepository : IRepository<Contacts>
    {
        Contacts GetContacts(int clientId);
    }
}