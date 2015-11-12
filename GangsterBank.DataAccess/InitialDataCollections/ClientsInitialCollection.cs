using System.Collections.Generic;
using System.Collections.ObjectModel;
using GangsterBank.Domain.Entities.Accounts;
using GangsterBank.Domain.Entities.Clients;

namespace GangsterBank.DataAccess.InitialDataCollections
{
    using GangsterBank.Domain.Entities.Clients.TakenLoan;

    internal static partial class InitialDataCollections
    {
        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                UserName = "Ceka",
                Accounts = new Collection<Account>(),
                FirstName = "Sergey",
                LastName = "Kotugin",
                TakenLoans = new Collection<TakenLoan>(),
                Email = "s.kotugin@revotechs.com",
                Obligations = new Collection<Obligation>(),
                Properties = new Collection<Property>(),
                PersonalDetails = new PersonalDetails(),
                PasswordHash = "AHwaMGqnpPkRFzUWeeNQwwHINvmfTFI/RO06LVJbNbSqWydYPAHD8TPrYBONu/q/+g=="
            }
        };
    }
}
