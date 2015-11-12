using System;
using GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans
{
    public class OneTimePaymentCreditPlanBusinessLogicEntity : BaseCreditPlanBusinessLogicEntity
    {
        public OneTimePaymentCreditPlanBusinessLogicEntity()
        {
            MontlyPaymentLogic = (creditSum, percents, currentMonths) => currentMonths == this.CreditPeriodInMonths ? this.GetTotalCreditPlanPayment() : 0;
        }

        public override decimal GetTotalCreditPlanPayment()
        {
            return CreditSum + CreditSum * CreditPeriodInMonths * Percents / 100 / 12;
        }
    }
}