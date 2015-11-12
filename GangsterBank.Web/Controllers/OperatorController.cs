namespace GangsterBank.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Models.Operator;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    [GangsterBankAuthorize(Role.Operator, Role.Administrator)]
    public class OperatorController : BaseController
    {
        #region Fields

        private readonly IClientsService clientsService;

        private readonly IClientProfileService clientProfileService;

        #endregion

        #region Constructors and Destructors

        public OperatorController(IUserContext userContext, IClientsService clientsService, IClientProfileService clientProfileService)
            : base(userContext)
        {
            Contract.Requires<ArgumentNullException>(clientsService.IsNotNull());
            Contract.Requires<ArgumentNullException>(clientProfileService.IsNotNull());
            this.clientsService = clientsService;
        }

        #endregion

        #region Public Methods and Operators

        public void ConfirmClient(int clientId)
        {
            Client client = this.clientsService.GetClient(clientId);
            this.clientsService.ConfirmClient(client);
        }

        public JsonResult GetUnconfirmedClients([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Client> unconfirmedClients = this.clientsService.GetClientsForApproval();
            IEnumerable<UnconfirmedClientViewModel> models =
                unconfirmedClients.Select(this.MapToUnconfirmedClientViewModel);
            return this.Json(models.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return this.View(this._userContext.User.Id);
        }

        public ActionResult UnconfirmedClients()
        {
            return this.View();
        }

        #endregion

        #region Methods

        private static string GetAddressText(Address address)
        {
            if (address.IsNull())
            {
                return "Not set";
            }

            return string.Format(
                "{0}, {1}; {2} {3}-{4}", 
                ValueOrUnknown(address.City.Name), 
                ValueOrUnknown(address.Country.Name), 
                ValueOrUnknown(address.Street), 
                ValueOrUnknown(address.HouseNumber), 
                ValueOrUnknown(address.FlatNumber));
        }

        private static string GetEmploymentText(EmploymentData employmentData)
        {
            if (employmentData.IsUnemployed)
            {
                return "Unemployed";
            }

            return string.Format(
                "{0} at {1} from {2}, {3}", 
                ValueOrUnknown(employmentData.Position), 
                ValueOrUnknown(employmentData.Company), 
                ValueOrUnknown(employmentData.HireDate), 
                ValueOrUnknown(employmentData.Salary));
        }

        private static string GetNameText(User client)
        {
            return string.Format("{0} {1}", client.LastName, client.FirstName);
        }

        private static string GetPassportText(PassportData passportData)
        {
            return string.Format(
                "{0}, issued by {1} on {2}, valid till {3}", 
                ValueOrUnknown(passportData.PassportNumber), 
                ValueOrUnknown(passportData.Issuer), 
                ValueOrUnknown(passportData.IssueDate), 
                ValueOrUnknown(passportData.ExpirationDate));
        }

        private static string ValueOrUnknown(object value)
        {
            return value == null || string.IsNullOrWhiteSpace(value.ToString()) ? string.Empty : value.ToString();
        }

        private UnconfirmedClientViewModel MapToUnconfirmedClientViewModel(Client client)
        {
            var model = new UnconfirmedClientViewModel
                            {
                                ClientId = client.Id, 
                                BirthDate = client.PersonalDetails.BirthDate, 
                                Employment =
                                    GetEmploymentText(client.PersonalDetails.EmploymentData), 
                                Name = GetNameText(client), 
                                Passport = GetPassportText(client.PersonalDetails.PassportData), 
                                PhoneNumber = client.PersonalDetails.Contacts.PhoneNumber, 
                                RegistrationAddress =
                                    GetAddressText(
                                        client.PersonalDetails.Contacts.RegistrationAddress)
                            };
            return model;
        }

        #endregion
    }
}