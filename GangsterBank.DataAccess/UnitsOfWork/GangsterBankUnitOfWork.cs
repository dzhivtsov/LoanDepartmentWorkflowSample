namespace GangsterBank.DataAccess.UnitsOfWork
{
    using System;
    using System.Diagnostics.Contracts;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Credits;

    public class GangsterBankUnitOfWork : IGangsterBankUnitOfWork
    {
        #region Constructors and Destructors

        public GangsterBankUnitOfWork(
            IRepository<Account> accountsRepository, 
            IRepository<Address> addressesRepository, 
            ICitiesRepository citiesRepository, 
            IClientsRepository clientsRepository, 
            IContactsRepository contactsRepository, 
            ICountriesRepository countryRepository, 
            IEmploymentDataRepository employmentDataRepository, 
            ILoanProductsRepository loanProductsRepository, 
            IObligationRepository obligationsRepository, 
            IPassportDataRepository passportDataRepository, 
            IPropertyRepository propertiesRepository,
            IPersonalDetailsRepository personalDetailsRepository,
            ILoanRequestsRepository loanRequestsRepository,
            IRolesRepository roles,
            IRepository<LoanProductRequirements> loanProductsRequirmentsRepository,
            IRepository<TakenLoan> takenLoansRepository,
            IRepository<Payment> paymentsRepository,
            ILoanPaymentsRepository loanPaymentsRepository)
        {
            Contract.Requires<ArgumentNullException>(accountsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(addressesRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(citiesRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(clientsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(contactsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(countryRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(employmentDataRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanProductsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(obligationsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(passportDataRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(propertiesRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(personalDetailsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanRequestsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(roles.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanProductsRequirmentsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(takenLoansRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(paymentsRepository.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanPaymentsRepository.IsNotNull());

            this.AccountsRepository = accountsRepository;
            this.AddressesRepository = addressesRepository;
            this.CitiesRepository = citiesRepository;
            this.ClientsRepository = clientsRepository;
            this.ContactsRepository = contactsRepository;
            this.CountryRepository = countryRepository;
            this.EmploymentDataRepository = employmentDataRepository;
            this.LoanProductsRepository = loanProductsRepository;
            this.ObligationsRepository = obligationsRepository;
            this.PassportDataRepository = passportDataRepository;
            this.PropertiesRepository = propertiesRepository;
            this.PersonalDetailsRepository = personalDetailsRepository;
            this.LoanRequestsRepository = loanRequestsRepository;
            this.RolesRepository = roles;
            this.LoanProductsRequirmentsRepository = loanProductsRequirmentsRepository;
            this.TakenLoansRepository = takenLoansRepository;
            this.PaymentsRepository = paymentsRepository;
            this.LoanPaymentsRepository = loanPaymentsRepository;
        }

        #endregion

        #region Public Properties

        public IRepository<Account> AccountsRepository { get; private set; }

        public IRepository<Address> AddressesRepository { get; private set; }

        public ICitiesRepository CitiesRepository { get; private set; }

        public IClientsRepository ClientsRepository { get; private set; }

        public IContactsRepository ContactsRepository { get; private set; }

        public ICountriesRepository CountryRepository { get; private set; }

        public IEmploymentDataRepository EmploymentDataRepository { get; private set; }

        public ILoanProductsRepository LoanProductsRepository { get; private set; }

        public ILoanRequestsRepository LoanRequestsRepository { get; private set; }

        public IObligationRepository ObligationsRepository { get; private set; }

        public IPassportDataRepository PassportDataRepository { get; private set; }

        public IPersonalDetailsRepository PersonalDetailsRepository { get; private set; }

        public IPropertyRepository PropertiesRepository { get; private set; }

        public IRolesRepository RolesRepository { get; set; }

        public IRepository<LoanProductRequirements> LoanProductsRequirmentsRepository { get; set; }

        public IRepository<TakenLoan> TakenLoansRepository { get; private set; }

        public IRepository<Payment> PaymentsRepository { get; private set; }

        public ILoanPaymentsRepository LoanPaymentsRepository { get; private set; }

        #endregion
    }
}