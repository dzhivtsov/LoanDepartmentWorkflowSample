namespace GangsterBank.BusinessLogic
{
    using System;
    using System.Diagnostics.Contracts;

    using GangsterBank.DataAccess.UnitsOfWork;

    public class ClientsService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        #endregion

        #region Constructors and Destructors

        public ClientsService(IGangsterBankUnitOfWork gangsterBankUnitOfWork)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork != null);
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
        }

        #endregion
    }
}