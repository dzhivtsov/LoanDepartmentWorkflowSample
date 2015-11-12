namespace GangsterBank.BusinessLogic.Contracts.Credits
{
    using System.Collections.Generic;

    using GangsterBank.Domain.BusinessLogicEntities.CreditPlans;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;

    public interface ICreditManager
    {
        /// <summary>
        /// Returns calculated payments for all credits
        /// </summary>
        /// <param name="takenLoans"></param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<TakenLoan, IEnumerable<decimal>>> GetMonthlyPayments(IEnumerable<TakenLoan> takenLoans);

        /// <summary>
        /// Returns calculated payments for credit
        /// </summary>
        /// <param name="takenLoan"></param>
        /// <returns></returns>
        IEnumerable<decimal> GetMonthlyPayments(TakenLoan takenLoan);

        /// <summary>
        /// Returns calculated result for loan product with specified amount and period
        /// </summary>
        /// <param name="loanProductId"></param>
        /// <param name="amount"></param>
        /// <param name="monthes"></param>
        /// <returns></returns>
        CalculateCreditResult CalculateCredit(int loanProductId, decimal amount, int monthes);
    }
}
