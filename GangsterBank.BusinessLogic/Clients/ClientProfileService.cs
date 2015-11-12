namespace GangsterBank.BusinessLogic.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Security.Authentication.ExtendedProtection;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients;

    public class ClientProfileService : IClientProfileService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        #endregion

        #region Constructors and Destructors

        public ClientProfileService(IGangsterBankUnitOfWork gangsterBankUnitOfWork)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork.IsNotNull());
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        #endregion

        #region Public Methods and Operators

        public Client GetClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            Client client = this.gangsterBankUnitOfWork.ClientsRepository.GetById(clientId);
            return client;
        }

        public Contacts GetClientContacts(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            Contacts contacts = this.gangsterBankUnitOfWork.ContactsRepository.GetContacts(clientId);
            return contacts;
        }

        public EmploymentData GetClientEmploymentData(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            EmploymentData employmentData =
                this.gangsterBankUnitOfWork.EmploymentDataRepository.GetClientEmployment(clientId);
            return employmentData;
        }

        public PersonalDetails GetClientPersonalDetails(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            PersonalDetails personalDetails =
                this.gangsterBankUnitOfWork.PersonalDetailsRepository.GetPersonalDetails(clientId);
            return personalDetails;
        }

        public void Save(Contacts contacts)
        {
            Contract.Requires<ArgumentNullException>(contacts.IsNotNull());
            this.gangsterBankUnitOfWork.ContactsRepository.CreateOrUpdate(contacts);
        }

        public void Save(EmploymentData employment)
        {
            Contract.Requires<ArgumentNullException>(employment.IsNotNull());
            if (employment.IsUnemployed)
            {
                employment.Company = null;
                employment.HireDate = null;
                employment.Position = null;
                employment.Salary = null;
            }

            this.gangsterBankUnitOfWork.EmploymentDataRepository.CreateOrUpdate(employment);
        }

        public PassportData GetClientPassportData(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            PassportData passportData =
                this.gangsterBankUnitOfWork.PassportDataRepository.GetPassportDataForClient(clientId);
            return passportData;
        }

        public void Save(PassportData passportData)
        {
            Contract.Requires<ArgumentNullException>(passportData.IsNotNull());
            this.gangsterBankUnitOfWork.PassportDataRepository.CreateOrUpdate(passportData);
        }

        public IEnumerable<Property> GetClientProperties(int clientId)
        {
            Contract.Requires<ArgumentException>(clientId.IsPositive());
            IEnumerable<Property> properties =
                this.gangsterBankUnitOfWork.PropertiesRepository.GetPropertiesForClient(clientId);
            
            return properties;
        }

        public void Save(Client client)
        {
            Contract.Requires<ArgumentNullException>(client.IsNotNull());
            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(client);
        }

        public Property GetClientProperty(int propertyId)
        {
            Contract.Requires<ArgumentException>(propertyId.IsPositive());
            Property property = this.gangsterBankUnitOfWork.PropertiesRepository.GetById(propertyId);
            
            return property;
        }

        public void Save(Property property)
        {
            Contract.Requires<ArgumentException>(property.IsNotNull());
            this.gangsterBankUnitOfWork.PropertiesRepository.CreateOrUpdate(property);
        }

        public IEnumerable<Obligation> GetClientObligations(int clientId)
        {
            Contract.Requires<ArgumentException>(clientId.IsPositive());
            IEnumerable<Obligation> obligations =
                this.gangsterBankUnitOfWork.ObligationsRepository.GetObligationsForClient(clientId);

            return obligations;
        }

        public Obligation GetClientObligation(int obligationId)
        {
            Contract.Requires<ArgumentException>(obligationId.IsPositive());
            Obligation obligation = this.gangsterBankUnitOfWork.ObligationsRepository.GetById(obligationId);

            return obligation;
        }
        public void Save(Obligation obligation)
        {
            Contract.Requires<ArgumentNullException>(obligation.IsNotNull());
            this.gangsterBankUnitOfWork.ObligationsRepository.CreateOrUpdate(obligation);
        }


        #endregion
    }
}