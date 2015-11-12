using System.Linq;

namespace GangsterBank.DataAccess.Migrations
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Text;

    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.Membership;

    #endregion

    internal sealed class Configuration : DbMigrationsConfiguration<GangsterBankContext>
    {
        #region Constructors and Destructors

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        #endregion

        #region Methods

        protected override void Seed(GangsterBankContext context)
        {
            AddRoles(context);
            AddLoanProducts(context);
            AddClients(context);
            base.Seed(context);
        }

        /// <summary>
        /// Adds test user with username "test_client" and password "123456", the user belongs to all roles
        /// </summary>
        private static void AddClients(GangsterBankContext context)
        {
            DeleteAllEntities<Payment>(context);
            DeleteAllEntities<Property>(context);
            DeleteAllEntities<Client>(context);
            
            var client1 = new Client
            {
                Id = 1,
                Email = "d.zhivtsov@revotechs.com",
                FirstName = "Administrator",
                LastName = "TestClient",
                UserName = "Administrator",
                PasswordHash =
                    "AHwaMGqnpPkRFzUWeeNQwwHINvmfTFI/RO06LVJbNbSqWydYPAHD8TPrYBONu/q/+g==",
                SecurityStamp = "aea8f068-15fb-4567-83c1-5b97faa5155e",
                IsConfirmed = true,
                PersonalDetails =
                    new PersonalDetails
                    {
                        PassportData = new PassportData
                        {
                            PassportNumber = "MP2228538",
                            PersonalNumber = "7777777A444PB1"
                        },
                        EmploymentData = new EmploymentData
                        {
                            Salary = 100000,
                            HireDate = DateTime.Now.AddMonths(-100),
                        },
                        Contacts = new Contacts
                        {
                            RegistrationAddress = new Address
                            {
                                City = new City
                                {
                                    Name = "Minsk"
                                }
                            }
                        }
                    }
            };

            var clientRoleName1 = Role.Administrator.ToString();
            var clinetRoleId1 = context.Roles.FirstOrDefault(x => x.Name == clientRoleName1).Id;
            client1.Roles.Add(new IdentityUserRoleEntity
            {
                RoleId = clinetRoleId1
            });
            context.Clients.Add(client1);

            for (int i = 0; i < 250; i++)
            {
                var client = new Client
                {
                    Id = 1,
                    Email = "d.zhivtsov@revotechs.com",
                    FirstName = "TestClient" + i,
                    LastName = "TestClient",
                    UserName = "test_client" + i,
                    PasswordHash =
                        "AHwaMGqnpPkRFzUWeeNQwwHINvmfTFI/RO06LVJbNbSqWydYPAHD8TPrYBONu/q/+g==",
                    SecurityStamp = "aea8f068-15fb-4567-83c1-5b97faa5155e",
                    IsConfirmed = true,
                    PersonalDetails =
                        new PersonalDetails
                        {
                            PassportData = new PassportData
                                               {
                                                   PassportNumber = "MP2228538",
                                                   PersonalNumber = "7777777A444PB1"
                                               },
                            EmploymentData = new EmploymentData
                                                 {
                                                     Salary = 100000,
                                                     HireDate = DateTime.Now.AddMonths(-100),
                                                 },
                            Contacts = new Contacts { RegistrationAddress = new Address
                                                                                {
                                                                                    City = new City
                                                                                               {
                                                                                                   Name = "Minsk"
                                                                                               }
                                                                                }
                            }
                        }
                };

                var clientRoleName = Role.Client.ToString();
                var clinetRoleId = context.Roles.FirstOrDefault(x => x.Name == clientRoleName).Id;
                client.Roles.Add(new IdentityUserRoleEntity
                {
                    RoleId = clinetRoleId
                });
                context.Clients.Add(client);

                client = new Client
                {
                    Id = 2,
                    Email = "d.zhivtsov@revotechs.com",
                    FirstName = "TestClient1_" + i,
                    LastName = "TestClient1",
                    UserName = "test_client1" + i,
                    PasswordHash =
                        "AHwaMGqnpPkRFzUWeeNQwwHINvmfTFI/RO06LVJbNbSqWydYPAHD8TPrYBONu/q/+g==",
                    SecurityStamp = "aea8f068-15fb-4567-83c1-5b97faa5155e",
                    IsConfirmed = true,
                    PersonalDetails =
                        new PersonalDetails
                        {
                            PassportData = new PassportData(),
                            EmploymentData = new EmploymentData(),
                            Contacts = new Contacts()
                        }
                };
                client.Roles.Add(new IdentityUserRoleEntity
                {
                    RoleId = clinetRoleId
                });
                context.Clients.Add(client);
            }
            
            SaveChanges(context);
        }

        private static void AddLoanProducts(GangsterBankContext context)
        {
            DeleteAllEntities<LoanPayment>(context);
            DeleteAllEntities<LoanRequest>(context);
            DeleteAllEntities<TakenLoan>(context);
            DeleteAllEntities<LoanProduct>(context);
            DeleteAllEntities<LoanProductRequirements>(context);
            AddLoanProducts(context, InitialDataCollections.InitialDataCollections.LoanProducts);
        }

        private static void AddLoanProducts(GangsterBankContext context, IEnumerable<LoanProduct> products)
        {
            context.LoanProducts.AddRange(products);
        }

        private static void AddRoles(GangsterBankContext context)
        {
            DeleteAllEntities<IdentityRoleEntity>(context);
            string[] allRoleNames = Enum.GetNames(typeof(Role));
            AddRoles(context, allRoleNames);
        }

        private static void AddRoles(GangsterBankContext context, IEnumerable<string> roleNames)
        {
            foreach (string roleName in roleNames)
            {
                context.Roles.Add(new IdentityRoleEntity { Name = roleName });
            }

            SaveChanges(context);
        }

        private static void DeleteAllEntities<T>(DbContext context) where T : class
        {
            DbSet<T> set = context.Set<T>();
            foreach (T entity in set)
            {
                set.Remove(entity);
            }

            SaveChanges(context);
        }

        private static void DeleteAllExistingClients(GangsterBankContext context)
        {
            context.Clients.RemoveRange(context.Clients);
            SaveChanges(context);
        }

        private static void AddClients(GangsterBankContext context, IEnumerable<Client> clients)
        {
            clients = clients.ToArray();
            foreach (var client in clients)
            {
                foreach (var identityRoleEntity in context.Roles)
                {
                    client.Roles.Add(new IdentityUserRoleEntity { RoleId = identityRoleEntity.Id });
                }
            }
            context.Clients.AddRange(clients);
        }

        private static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (DbEntityValidationResult failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (DbValidationError error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
            }
        }

        #endregion
    }
}