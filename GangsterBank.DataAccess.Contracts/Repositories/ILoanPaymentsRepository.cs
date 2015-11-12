namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;

    public interface ILoanPaymentsRepository : IRepository<LoanPayment>
    {
        IEnumerable<LoanPayment> GetByStatusAndDate(LoanPaymentStatus status, DateTime date);
    }
}