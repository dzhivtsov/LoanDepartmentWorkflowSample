namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class PersonalDetailsRepository : BaseRepository<PersonalDetails>, IPersonalDetailsRepository
    {
        public PersonalDetailsRepository(GangsterBankContext context)
            : base(context)
        {
        }

        public PersonalDetails GetPersonalDetails(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            PersonalDetails personalDetails =
                (from client in this.Context.Clients where client.Id == clientId select client.PersonalDetails)
                    .SingleOrDefault();
            this.ThrowNotFoundException(personalDetails);
            return personalDetails;
        }
    }
}