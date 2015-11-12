using System;
using GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base;

namespace GangsterBank.Domain.BusinessLogicEntities.CreditPlans
{
    public class AnuitetCreditPlanBusinessLogicEntity: BaseCreditPlanBusinessLogicEntity
    {
        public AnuitetCreditPlanBusinessLogicEntity()
        {
            InitMonthlypaymentLogic();
        }

        public AnuitetCreditPlanBusinessLogicEntity(int periodInMonths, decimal sum, int percents)
        {
            CreditPeriodInMonths = periodInMonths;
            CreditSum = sum;
            Percents = percents;
            InitMonthlypaymentLogic();
        }

        private void InitMonthlypaymentLogic()
        {
            var monthlyPayment = this.CalculateMonthlyPayment();
            this.MontlyPaymentLogic = (creditSum, percents, currentMonths) => monthlyPayment;
        }

        public override decimal GetTotalCreditPlanPayment()
        {
            return this.CalculateMonthlyPayment() * this.CreditPeriodInMonths;
        }

        private decimal CalculateMonthlyPayment()
        {
            var monthlyPercents = (double)Percents / 12 / 100;
            var monthlyPercentsMultiplier = Math.Pow(1 + monthlyPercents, -CreditPeriodInMonths);
            var k = monthlyPercents / (1 - monthlyPercentsMultiplier);
            var monthlyPayment = (decimal)((double)CreditSum * k);
            return monthlyPayment;
        }

    }
}
