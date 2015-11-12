using GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans
{
    public class DifferentialCreditPlanBusinessLogicEntity: BaseCreditPlanBusinessLogicEntity
    {
        public DifferentialCreditPlanBusinessLogicEntity()
        {
            MontlyPaymentLogic = (creditSum, percents, currentMonths) => creditSum / CreditPeriodInMonths + creditSum * (CreditPeriodInMonths - currentMonths) * percents / 100 / 12;
        }

        public override decimal GetTotalCreditPlanPayment()
        {
            return CreditSum + CreditSum*Percents/12/100*(CreditPeriodInMonths*(CreditPeriodInMonths + 1))/2;
        }
    }
}
