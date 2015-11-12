namespace GangsterBank.Web.Infrastructure.Managers
{
    using System.Web;

    using GangsterBank.Web.Models.ClientProfile;

    public interface IClientProfileManager
    {
        #region Public Methods and Operators

        BasicDetailsViewModel GetBasicDetailsViewModel(int clientId);

        ContactsViewModel GetContactsViewModel(int resolvedClientId);

        EmploymentViewModel GetEmploymentViewModel(int resolvedClientId);

        ObligationsViewModel GetObligationsViewModel(int resolvedClientId);

        PassportViewModel GetPassportViewModel(int clientId);

        byte[] GetClientDigitalCopy(int clientId);

        PropertiesViewModel GetPropertiesViewModel(int clientId);

        void SaveBasicDetails(BasicDetailsViewModel model);

        void SaveContacts(ContactsViewModel contacts);

        void SaveEmployment(EmploymentViewModel model);

        void SaveObligations(ObligationsViewModel model);

        void SavePassportData(PassportViewModel model, HttpPostedFileBase digitalCopy);

        void SaveProperty(PropertyViewModel model);

        void RemoveProperty(int propertyId);

        PropertyViewModel GetProperty(int clientId, int propertyId);

        ObligationViewModel GetObligation(int clientId, int obligationId);

        void SaveObligation(ObligationViewModel model);

        void RemoveObligation(int obligationId);

        #endregion
    }
}