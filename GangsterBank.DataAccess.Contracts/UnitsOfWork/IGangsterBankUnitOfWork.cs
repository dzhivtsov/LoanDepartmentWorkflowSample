namespace GangsterBank.DataAccess.Contracts.UnitsOfWork
{
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Credits;

    public interface IGangsterBankUnitOfWork
    {
        #region Public Properties

        IRepository<Account> AccountsRepository { get; }

        IRepository<Address> AddressesRepository { get; }

        ICitiesRepository CitiesRepository { get; }

        IClientsRepository ClientsRepository { get; }

        IContactsRepository ContactsRepository { get; }

        ICountriesRepository CountryRepository { get; }

        IEmploymentDataRepository EmploymentDataRepository { get; }

        ILoanProductsRepository LoanProductsRepository { get; }

        ILoanRequestsRepository LoanRequestsRepository { get; }

        IObligationRepository ObligationsRepository { get; }

        IPassportDataRepository PassportDataRepository { get; }

        IPersonalDetailsRepository PersonalDetailsRepository { get; }

        IPropertyRepository PropertiesRepository { get; }

        IRolesRepository RolesRepository { get; }

        IRepository<LoanProductRequirements> LoanProductsRequirmentsRepository { get; }

        IRepository<TakenLoan> TakenLoansRepository { get; }

        IRepository<Payment> PaymentsRepository { get; }

        ILoanPaymentsRepository LoanPaymentsRepository { get; }

        #endregion
    }
}