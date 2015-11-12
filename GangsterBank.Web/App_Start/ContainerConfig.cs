namespace GangsterBank.Web
{
    #region

    using System.Data.Entity;

    using Autofac;
    using Autofac.Integration.Mvc;

    using GangsterBank.BusinessLogic.Accounts;
    using GangsterBank.BusinessLogic.Clients;
    using GangsterBank.BusinessLogic.Contracts.Accounts;
    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.EmailTemplates;
    using GangsterBank.BusinessLogic.Contracts.Info;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.BusinessLogic.Contracts.Membership;
    using GangsterBank.BusinessLogic.Contracts.Statistics;
    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;
    using GangsterBank.BusinessLogic.Credits;
    using GangsterBank.BusinessLogic.Credits.RequestPrerequisiteRules;
    using GangsterBank.BusinessLogic.EmailTemplates;
    using GangsterBank.BusinessLogic.Info;
    using GangsterBank.BusinessLogic.Mail;
    using GangsterBank.BusinessLogic.Membership;
    using GangsterBank.BusinessLogic.Statistics;
    using GangsterBank.BusinessLogic.Tasks.Daily;
    using GangsterBank.DataAccess;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.DataAccess.Repositories;
    using GangsterBank.DataAccess.UnitsOfWork;
    using GangsterBank.Domain.Workflow;
    using GangsterBank.Web.Infrastructure.Contexts;
    using GangsterBank.Web.Infrastructure.Helpers.Membership;
    using GangsterBank.Web.Infrastructure.Managers;

    using IStatisticsManager = GangsterBank.Web.Infrastructure.Managers.IStatisticsManager;
    using StatisticsManager = GangsterBank.Web.Infrastructure.Managers.StatisticsManager;

    #endregion

    public class ContainerConfig
    {
        #region Public Methods and Operators

        public static void RegisterTypes(ContainerBuilder builder)
        {
            RegisterDataAccessTypes(builder);
            RegisterBusinessLogicTypes(builder);
            RegisterWebApplicationTypes(builder);
        }

        #endregion

        #region Methods

        private static void RegisterBusinessLogicTypes(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ClientProfileService>().As<IClientProfileService>();
            builder.RegisterType<CreditService>().As<ICreditService>();
            builder.RegisterType<CitiesService>().As<ICitiesService>();
            builder.RegisterType<CountriesService>().As<ICountriesService>();
            builder.RegisterType<CurrentUserRetriever>().As<ICurrentUserRetriever>();
            builder.RegisterType<CreditManager>().As<ICreditManager>();
            builder.RegisterType<LoanRequestsService>().As<ILoanRequestsService>();
            builder.RegisterType<CompositeLoanRequestPrerequisiteRule>().As<ILoanRequestPrerequisiteRule>();
            builder.RegisterType<LoanProductCreationWorkflowConfiguration>().As<LoanProductCreationWorkflowConfiguration>();
            builder.RegisterType<ClientsService>().As<IClientsService>();
            builder.RegisterType<DailyTaskManager>().As<IDailyTaskManager>();
            builder.RegisterType<CalculateFineTask>().As<CalculateFineTask>();
            builder.RegisterType<PaymentService>().As<IPaymentService>();
            builder.RegisterType<SendPaymentNotification>().As<SendPaymentNotification>();
            builder.RegisterType<MailService>().As<IMailService>();
            builder.RegisterType<StatisticsManager>().As<IStatisticsManager>();
            builder.RegisterType<BusinessLogic.Statistics.StatisticsManager>().As<BusinessLogic.Contracts.Statistics.IStatisticsManager>();
            builder.RegisterType<PayLoanPaymentsTask>().As<PayLoanPaymentsTask>();
            builder.RegisterType<EmailTemplatesService>().As<IEmailTemplatesService>();
        }

        private static void RegisterDataAccessTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GangsterBankContext>().InstancePerHttpRequest();
            builder.RegisterType<GangsterBankContext>().As<DbContext>().InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType<LoanProductsRepository>().As<ILoanProductsRepository>();
            builder.RegisterType<PersonalDetailsRepository>().As<IPersonalDetailsRepository>();
            builder.RegisterType<EmploymentDataRepository>().As<IEmploymentDataRepository>();
            builder.RegisterType<CitiesRepository>().As<ICitiesRepository>();
            builder.RegisterType<CountriesRepository>().As<ICountriesRepository>();
            builder.RegisterType<ContactsRepository>().As<IContactsRepository>();
            builder.RegisterType<GangsterBankUnitOfWork>().As<IGangsterBankUnitOfWork>();
            builder.RegisterType<UserEntityStore>().As<IUserEntityStore>();
            builder.RegisterType<PassportDataRepository>().As<IPassportDataRepository>();
            builder.RegisterType<PropertyRepository>().As<IPropertyRepository>();
            builder.RegisterType<ObligationRepository>().As<IObligationRepository>();
            builder.RegisterType<LoanRequestsRepository>().As<ILoanRequestsRepository>();
            builder.RegisterType<RolesRepository>().As<IRolesRepository>();
            builder.RegisterType<ClientsRepository>().As<IClientsRepository>();
            builder.RegisterType<LoanPaymentsRepository>().As<ILoanPaymentsRepository>();
            builder.RegisterType<EmailTemplatesRepository>().As<IEmailTemplatesRepository>();
        }

        private static void RegisterWebApplicationTypes(ContainerBuilder builder)
        {
            builder.RegisterType<UserContext>().As<IUserContext>();
            builder.RegisterType<ClientProfileManager>().As<IClientProfileManager>();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }

        #endregion
    }
}