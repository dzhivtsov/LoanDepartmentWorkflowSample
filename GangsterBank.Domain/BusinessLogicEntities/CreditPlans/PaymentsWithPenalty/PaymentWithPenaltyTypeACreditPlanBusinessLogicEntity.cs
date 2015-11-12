using GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans.PaymentsWithPenalty
{
    public class PaymentWithPenaltyTypeACreditPlanBusinessLogicEntity : BasePaymentWithPenaltyCreditPlanBusinessLogicEntity
    {
        public PaymentWithPenaltyTypeACreditPlanBusinessLogicEntity()
        {
            PenaltyMonths = CreditPeriodInMonths/2;
            PenaltyPercent = 7;
        }
    }
}
