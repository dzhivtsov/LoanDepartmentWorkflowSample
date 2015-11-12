namespace GangsterBank.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;

    public class LoanPaymentsRepository : BaseRepository<LoanPayment>, ILoanPaymentsRepository
    {
        #region Constructors and Destructors

        public LoanPaymentsRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<LoanPayment> GetByStatusAndDate(LoanPaymentStatus status, DateTime date)
        {
            IQueryable<LoanPayment> loanPayments = from loanPayment in this.ActiveEntities
                                                   where loanPayment.Status == status && loanPayment.Date == date
                                                   select loanPayment;
            return loanPayments;
        }

        #endregion
    }
}