namespace GangsterBank.DataAccess
{
    #region

    using System.Data.Entity;
    using System.Runtime.InteropServices;

    using GangsterBank.DataAccess.Conventions;
    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.EmailTemplates;
    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public class GangsterBankContext :
        IdentityDbContext
            <User, IdentityRoleEntity, int, IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity>
    {
        #region Public Properties

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<BankEmployee> BankEmployees { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Contacts> Contacts { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<EmploymentData> EmploymentData { get; set; }

        public DbSet<LoanProductRequirements> LoanProductRequirementses { get; set; }

        public DbSet<LoanProduct> LoanProducts { get; set; }

        public DbSet<LoanRequest> LoanRequests { get; set; }

        public DbSet<Obligation> Obligations { get; set; }

        public DbSet<PassportData> PassportData { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<TakenLoan> TakenLoans { get; set; }

        public DbSet<LoanPayment> LoanPayments { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DateTime2Convention());

            base.OnModelCreating(modelBuilder);
            ChangeIdentityModelTableNames(modelBuilder);
        }

        private static void ChangeIdentityModelTableNames(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRoleEntity>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLoginEntity>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaimEntity>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleEntity>().ToTable("Roles");
        }

        #endregion
    }
}