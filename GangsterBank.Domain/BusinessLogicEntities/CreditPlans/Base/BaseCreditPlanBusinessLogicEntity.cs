using System;
using System.Collections.Generic;
using GangsterBank.Domain.Entities.CreditPlans;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base
{
    public abstract class BaseCreditPlanBusinessLogicEntity : BaseCreditPlan, IBaseCreditPlanBusinessLogicEntity
    {
        protected Func<decimal, int, int, decimal> MontlyPaymentLogic;

        protected BaseCreditPlanBusinessLogicEntity()
        {
            MontlyPaymentLogic = (creditSum, percents, currentMonths) => creditSum * (1 + percents/100)/12;
        }

        public virtual decimal GetTotalCreditPlanPayment()
        {
            // Calculates with a simple formula : credit sum + year percents
            return CreditSum*(1 + Percents/100)*CreditPeriodInMonths/12;
        }

        public virtual IEnumerable<decimal> GetPaymentsByMonths()
        {
            var result = new List<decimal>();
            for (var i = 1; i <= CreditPeriodInMonths; i++)
            {
                result.Add(MontlyPaymentLogic.Invoke(CreditSum, Percents, i));
            }
            return result;
        }
    }
}
