using System;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base
{
    public abstract class BasePaymentWithPenaltyCreditPlanBusinessLogicEntity : BaseCreditPlanBusinessLogicEntity, IBasePaymentWithPenaltyCreditPlanBusinessLogicEntity
    {
        protected int PenaltyPercent { get; set; }
        protected int PenaltyMonths { get; set; }
        protected Func<int, decimal> PenaltyLogic;
        protected Func<int, bool> PossibilityForPenaltyPaymentLogic;

        protected BasePaymentWithPenaltyCreditPlanBusinessLogicEntity()
        {
            MontlyPaymentLogic = (creditSum, percents, currentMonths) => creditSum / CreditPeriodInMonths + creditSum * (CreditPeriodInMonths - currentMonths - 1) * percents / 12;
            PenaltyLogic = (month) => (CreditPeriodInMonths - month)*CreditSum*(Percents + PenaltyPercent)/100;
            PossibilityForPenaltyPaymentLogic = (month) => CreditPeriodInMonths/2 <= PenaltyMonths;
        }

        public override decimal GetTotalCreditPlanPayment()
        {
            return CreditSum + CreditSum*Percents/12*(CreditPeriodInMonths*(CreditPeriodInMonths + 1))/2;
        }

        public virtual decimal GetPenalty(int month)
        {
            //S.K. suggest to use exceptions
            if (PossibilityOfPaymentWithPenalty(month))
            {
                return PenaltyLogic(month);
            }
            return -1;
        }

        public virtual decimal GetPaymentWithPenalty(int month)
        {
            return CreditSum*(CreditPeriodInMonths - month) + GetPenalty(month);
        }

        private bool PossibilityOfPaymentWithPenalty(int month)
        {
            return PossibilityForPenaltyPaymentLogic(month);
        }
    }
}
