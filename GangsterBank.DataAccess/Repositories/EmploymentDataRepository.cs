namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients;

    public class EmploymentDataRepository : BaseRepository<EmploymentData>, IEmploymentDataRepository
    {
        #region Constructors and Destructors

        public EmploymentDataRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public EmploymentData GetClientEmployment(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            EmploymentData employmentData =
                (from client in this.Context.Clients
                 where client.Id == clientId
                 select client.PersonalDetails.EmploymentData).SingleOrDefault();
            this.ThrowNotFoundException(employmentData);
            return employmentData;
        }

        #endregion
    }
}