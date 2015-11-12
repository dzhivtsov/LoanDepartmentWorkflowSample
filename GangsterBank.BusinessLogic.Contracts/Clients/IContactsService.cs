namespace GangsterBank.BusinessLogic.Contracts.Clients
{
    using GangsterBank.Domain.Entities.Clients;

    public interface IContactsService
    {
        #region Public Methods and Operators

        Contacts GetContactsForClient(int clientId);

        #endregion
    }
}