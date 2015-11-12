using System.Collections.Generic;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base
{
    public interface IBaseCreditPlanBusinessLogicEntity
    {
        /// <summary>
        /// Returns total price of credit
        /// </summary>
        /// <returns></returns>
        decimal GetTotalCreditPlanPayment();


        /// <summary>
        /// Return list of payments per each month
        /// </summary>
        /// <returns></returns>
        IEnumerable<decimal> GetPaymentsByMonths();
    }
}
