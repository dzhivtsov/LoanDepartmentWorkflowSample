namespace GangsterBank.Web.Infrastructure.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Security;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Info;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Repositories;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Web.Infrastructure.Extensions;
    using GangsterBank.Web.Models.ClientProfile;

    public class ClientProfileManager : IClientProfileManager
    {
        #region Fields

        private readonly ICitiesService citiesService;

        private readonly IClientProfileService clientProfileService;

        private readonly ICountriesService countriesService;

        private readonly IRepository<Property> propertyRepository;

        private readonly IObligationRepository obligationRepository;

        #endregion

        #region Constructors and Destructors

        public ClientProfileManager(
            IClientProfileService clientProfileService, 
            ICitiesService citiesService, 
            ICountriesService countriesService,
            IRepository<Property> propertyRepository,
            IObligationRepository obligationRepository)
        {
            Contract.Requires<ArgumentNullException>(clientProfileService.IsNotNull());
            Contract.Requires<ArgumentNullException>(citiesService.IsNotNull());
            Contract.Requires<ArgumentNullException>(countriesService.IsNotNull());

            this.clientProfileService = clientProfileService;
            this.citiesService = citiesService;
            this.countriesService = countriesService;
            this.propertyRepository = propertyRepository;
            this.obligationRepository = obligationRepository;
        }

        #endregion

        #region Public Methods and Operators

        public BasicDetailsViewModel GetBasicDetailsViewModel(int clientId)
        {
            Client client = this.clientProfileService.GetClient(clientId);
            var model = new BasicDetailsViewModel
                            {
                                ClientId = client.Id, 
                                FirstName = client.FirstName, 
                                LastName = client.LastName, 
                                BirthDate = client.PersonalDetails.BirthDate, 
                                Gender = client.PersonalDetails.Gender
                            };
            return model;
        }

        public ContactsViewModel GetContactsViewModel(int resolvedClientId)
        {
            Contacts contacts = this.clientProfileService.GetClientContacts(resolvedClientId);
            var model = new ContactsViewModel
                            {
                                ClientId = resolvedClientId, 
                                PhoneNumber = contacts.PhoneNumber, 
                                RegistrationAddress =
                                    MapToAddressViewModel(contacts.RegistrationAddress), 
                                ResidentialAddress = MapToAddressViewModel(contacts.ResidentialAddress)
                            };
            return model;
        }

        public PropertiesViewModel GetPropertiesViewModel(int resolvedClientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(resolvedClientId.IsPositive());
            IEnumerable<Property> properties = this.clientProfileService.GetClientProperties(resolvedClientId);

            var model = new PropertiesViewModel
                            {
                                ClientId = resolvedClientId,
                                Properties = MapToPropertyViewModel(properties.ToList())
                            };
            return model;
        }

        public void SaveBasicDetails(BasicDetailsViewModel model)
        {
            Client client = this.clientProfileService.GetClient(model.ClientId);
            client.FirstName = model.FirstName;
            client.LastName = model.LastName;
            client.PersonalDetails.BirthDate = model.BirthDate;
            client.PersonalDetails.Gender = model.Gender;
            client.Email = model.Email;
            this.clientProfileService.Save(client);
        }

        public EmploymentViewModel GetEmploymentViewModel(int clientId)
        {
            EmploymentData employmentData = this.clientProfileService.GetClientEmploymentData(clientId);
            var model = new EmploymentViewModel
                            {
                                ClientId = clientId,
                                IsUnemployed = employmentData.IsUnemployed,
                                Company = employmentData.Company,
                                HireDate = employmentData.HireDate,
                                Position = employmentData.Position,
                                Salary = employmentData.Salary
                            };
            return model;
        }

        public ObligationsViewModel GetObligationsViewModel(int resolvedClientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(resolvedClientId.IsPositive());
            IEnumerable<Obligation> obligations = this.clientProfileService.GetClientObligations(resolvedClientId);

            var model = new ObligationsViewModel
            {
                ClientId = resolvedClientId,
                Obligations = MapToObligationViewModel(obligations.ToList())
            };
            return model;
        }

        public void SaveEmployment(EmploymentViewModel model)
        {
            EmploymentData employment = this.clientProfileService.GetClientEmploymentData(model.ClientId);
            employment.Company = model.Company;
            employment.HireDate = model.HireDate;
            employment.Position = model.Position;
            employment.Salary = model.Salary;
            employment.IsUnemployed = model.IsUnemployed;
            this.clientProfileService.Save(employment);
        }

        public void SaveObligations(ObligationsViewModel model)
        {
            throw new NotImplementedException();
        }

        public PassportViewModel GetPassportViewModel(int clientId)
        {
            PassportData passportData = this.clientProfileService.GetClientPassportData(clientId);
            var model = new PassportViewModel
                            {
                                ClientId = clientId,
                                ExpirationDate = passportData.ExpirationDate,
                                IssueDate = passportData.IssueDate,
                                Issuer = passportData.Issuer,
                                PassportNumber = passportData.PassportNumber,
                                PersonalNumber = passportData.PersonalNumber,
                            };
            if ((passportData.DigitalCopy == null) || (passportData.DigitalCopy.Length == 0)) model.HasImage = false;
            else model.HasImage = true;
            return model;
        }

        public byte[] GetClientDigitalCopy(int clientId)
        {
            PassportData passportData = this.clientProfileService.GetClientPassportData(clientId);

            return passportData.DigitalCopy;
        }

        public void SavePassportData(PassportViewModel model, HttpPostedFileBase digitalCopy)
        {
            PassportData passportData = this.clientProfileService.GetClientPassportData(model.ClientId);
            passportData.ExpirationDate = model.ExpirationDate ?? DateTime.MinValue;
            passportData.IssueDate = model.IssueDate ?? DateTime.MinValue;
            passportData.Issuer = model.Issuer;
            passportData.PassportNumber = model.PassportNumber;
            passportData.PersonalNumber = model.PersonalNumber;
            if (digitalCopy != null)
                passportData.DigitalCopy = digitalCopy.ToByteArray();
            this.clientProfileService.Save(passportData);
        }

        public void SaveProperty(PropertyViewModel model)
        {
            Property property;
            if (model.PropertyId != 0)
                property = this.clientProfileService.GetClientProperty(model.PropertyId);
            else
                property = new Property();
            
            property.Cost = model.Cost;
            property.Description = model.Description;

            if (property.Id == 0)
            {
                Client client = this.clientProfileService.GetClient(model.ClientId);
                client.Properties.Add(property);
            }

            this.clientProfileService.Save(property);
        }

        public PropertyViewModel GetProperty(int clientId, int propertyId)
        {
            Property property = clientProfileService.GetClientProperty(propertyId);
            var model = new PropertyViewModel
                            {
                                Cost = property.Cost,
                                Description = property.Description,
                                ClientId = clientId,
                                PropertyId = propertyId
                            };

            return model;
        }

        public void RemoveProperty(int propertyId)
        {
            var property = propertyRepository.GetById(propertyId);
            propertyRepository.Remove(property);   
        }


        public void RemoveObligation(int obligationId)
        {
            Obligation obligation = obligationRepository.GetById(obligationId);
            obligationRepository.Remove(obligation);   
        }
        public ObligationViewModel GetObligation(int clientId, int obligationId)
        {
            Obligation obligation = clientProfileService.GetClientObligation(obligationId);

            var model = new ObligationViewModel();

            model.CompanyName = obligation.CompanyName;
            model.OutstandingAmount = obligation.OutstandingAmount;
            model.MonthlyPayments = obligation.MontlyPayments ?? default(decimal);
            model.ExpirationDate = obligation.ExpirationDate;
            model.DelayAmount = obligation.DelayAmount;
            model.ObligationId = obligation.Id;

            return model;
        }
        public void SaveObligation(ObligationViewModel model)
        {
            Obligation obligation;
            if (model.ObligationId != 0)
                obligation = this.clientProfileService.GetClientObligation(model.ObligationId);
            else
                obligation = new Obligation();

            obligation.CompanyName = model.CompanyName;
            obligation.OutstandingAmount = model.OutstandingAmount;
            obligation.MontlyPayments = model.MonthlyPayments;
            obligation.ExpirationDate = model.ExpirationDate ?? DateTime.Now;
            obligation.DelayAmount = model.DelayAmount;

            if (obligation.Id == 0)
            {
                Client client = this.clientProfileService.GetClient(model.ClientId);
                client.Obligations.Add(obligation);
            }

            this.clientProfileService.Save(obligation);
        }
        
        public void SaveContacts(ContactsViewModel model)
        {
            Contacts contacts = this.clientProfileService.GetClientContacts(model.ClientId);
            contacts.PhoneNumber = model.PhoneNumber;
            contacts.RegistrationAddress = this.MapAddress(model.RegistrationAddress, contacts.RegistrationAddress);
            contacts.ResidentialAddress = this.MapAddress(model.ResidentialAddress, contacts.ResidentialAddress);
            this.clientProfileService.Save(contacts);
        }

        #endregion

        #region Methods

        private static AddressViewModel MapToAddressViewModel(Address address)
        {
            if (address.IsNull())
            {
                return new AddressViewModel();
            }

            return new AddressViewModel
                       {
                           CaseNumber = address.CaseNumber, 
                           City = address.City != null ? address.City.Name : String.Empty, 
                           Country = address.Country != null ? address.Country.Name : String.Empty, 
                           FlatNumber = address.FlatNumber, 
                           HouseNumber = address.HouseNumber, 
                           PostIndex = address.PostIndex, 
                           Street = address.Street
                       };
        }

        private Address MapAddress(AddressViewModel addressViewModel, Address address)
        {
            address = address ?? new Address();
            address.CaseNumber = addressViewModel.CaseNumber;
            address.FlatNumber = addressViewModel.FlatNumber;
            address.HouseNumber = addressViewModel.HouseNumber ?? 0;
            address.PostIndex = addressViewModel.PostIndex;
            address.Street = addressViewModel.Street;
            address.City = this.citiesService.GetCityByName(addressViewModel.City);
            address.Country = this.countriesService.GetCountryByName(addressViewModel.Country);
            address.Country.Cities.Add(address.City);
            return address;
        }

        private IEnumerable<PropertyViewModel> MapToPropertyViewModel(List<Property> properties)
        {
            if (!properties.Any()) return Enumerable.Empty<PropertyViewModel>();

            var props = new List<PropertyViewModel>();
            foreach (var property in properties)
            {
                var model = new PropertyViewModel();
                model.Description = property.Description;
                model.Cost = property.Cost;
                model.PropertyId = property.Id;

                props.Add(model);
            }
            return props;
        }

        private IEnumerable<ObligationViewModel> MapToObligationViewModel(List<Obligation> obligations)
        {
            if (!obligations.Any()) return Enumerable.Empty<ObligationViewModel>();

            var obls = new List<ObligationViewModel>();
            foreach (var obligation in obligations)
            {
                var model = new ObligationViewModel();

                model.CompanyName = obligation.CompanyName;
                model.OutstandingAmount = obligation.OutstandingAmount;
                model.MonthlyPayments = obligation.MontlyPayments ?? default(decimal);
                model.ExpirationDate = obligation.ExpirationDate;
                model.DelayAmount = obligation.DelayAmount;
                model.ObligationId = obligation.Id;

                obls.Add(model);
            }
            return obls;
        }



        #endregion
    }
}