namespace GangsterBank.BusinessLogic.Contracts.Clients
{
    using System.Collections;
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;

    public interface IClientProfileService
    {
        PersonalDetails GetClientPersonalDetails(int clientId);

        Contacts GetClientContacts(int clientId);

        EmploymentData GetClientEmploymentData(int clientId);

        Client GetClient(int clientId);

        void Save(Client client);

        void Save(Contacts contacts);

        void Save(EmploymentData employment);

        PassportData GetClientPassportData(int clientId);

        void Save(PassportData passportData);

        IEnumerable<Property> GetClientProperties(int clientId);

        Property GetClientProperty(int propertyId);
        void Save(Property property);

        IEnumerable<Obligation> GetClientObligations(int clientId);

        Obligation GetClientObligation(int obligationId);

        void Save(Obligation obligation);
    }
}