namespace GangsterBank.BusinessLogic.Clients
{
    using System;
    using System.Diagnostics.Contracts;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients;

    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        public PersonalDetailsService(IGangsterBankUnitOfWork gangsterBankUnitOfWork)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork != null);
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        public PersonalDetails GetPersonalDetailsForClient(int clientId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(clientId.IsPositive());
            PersonalDetails personalDetails =
                this.gangsterBankUnitOfWork.PersonalDetailsRepository.GetPersonalDetails(clientId);
            return personalDetails;
        }
    }
}