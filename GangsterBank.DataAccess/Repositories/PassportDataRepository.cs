namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class PassportDataRepository : BaseRepository<PassportData>, IPassportDataRepository
    {
        #region Constructors and Destructors

        public PassportDataRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public PassportData GetPassportDataForClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            PassportData passportData =
                (from client in this.Context.Clients
                 where client.Id == clientId
                 select client.PersonalDetails.PassportData).SingleOrDefault();
            this.ThrowNotFoundException(passportData);
            return passportData;
        }

        #endregion
    }
}