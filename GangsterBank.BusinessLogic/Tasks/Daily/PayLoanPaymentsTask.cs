namespace GangsterBank.BusinessLogic.Tasks.Daily
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using GangsterBank.BusinessLogic.Contracts.Accounts;
    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;

    public class PayLoanPaymentsTask : IOperationalDayTask
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        private readonly IPaymentService paymentService;

        #endregion

        #region Constructors and Destructors

        public PayLoanPaymentsTask(IGangsterBankUnitOfWork gangsterBankUnitOfWork, IPaymentService paymentService)
        {
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
            this.paymentService = paymentService;
        }

        #endregion

        #region Public Methods and Operators

        public void Execute(IOperationalDayContext context)
        {
            IEnumerable<LoanPayment> loanPayments =
                this.gangsterBankUnitOfWork.LoanPaymentsRepository.GetByStatusAndDate(
                    LoanPaymentStatus.Active, 
                    context.CurrentDate.Date);
            foreach (LoanPayment loanPayment in loanPayments)
            {
                this.paymentService.TryPayLoanPayment(loanPayment);
            }
        }

        #endregion
    }
}