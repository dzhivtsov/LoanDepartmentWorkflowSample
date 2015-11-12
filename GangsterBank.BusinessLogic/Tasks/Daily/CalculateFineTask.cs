namespace GangsterBank.BusinessLogic.Tasks.Daily
{
    using System;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Credits;

    public class CalculateFineTask : IOperationalDayTask
    {
        private readonly ICreditService creditService;

        public CalculateFineTask(ICreditService creditService)
        {
            this.creditService = creditService;
        }

        public void Execute(IOperationalDayContext context)
        {
            var takenLoans = this.creditService.GetAllTakenLoans().Where(x => x.Status == TakenLoanStatus.Active);
            takenLoans.ForEach(x => this.ReCalcualteTakenLoanFine(x, context.CurrentDate));
        }

        private void ReCalcualteTakenLoanFine(TakenLoan takenLoan, DateTime date)
        {
            takenLoan.Payments.ForEach(x => this.ReCalculatePayment(x, date, takenLoan.ProductLoan));
        }

        private void ReCalculatePayment(LoanPayment payment, DateTime date, LoanProduct product)
        {
            if (payment.Date < date)
            {
                payment.Fine += product.FineDayPercentage * payment.Amount;
            }
        }
    }
}
