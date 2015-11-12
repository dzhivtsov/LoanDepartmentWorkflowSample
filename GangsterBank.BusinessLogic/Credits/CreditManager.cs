namespace GangsterBank.BusinessLogic.Credits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.BusinessLogicEntities.CreditPlans;
    using GangsterBank.Domain.BusinessLogicEntities.CreditPlans.Base;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Credits;

    public class CreditManager : ICreditManager
    {

        private readonly ICreditService creditService;

        public CreditManager(ICreditService creditService)
        {
            this.creditService = creditService;
        }

        public IEnumerable<KeyValuePair<TakenLoan, IEnumerable<decimal>>> GetMonthlyPayments(IEnumerable<TakenLoan> takenLoans)
        {
            var result = new List<KeyValuePair<TakenLoan, IEnumerable<decimal>>>();
            foreach (var takenLoan in takenLoans)
            {
                var credit = this.MapCreditPlanLogicEntityFromLoanProduct(takenLoan);
                result.Add(new KeyValuePair<TakenLoan, IEnumerable<decimal>>(takenLoan, credit.GetPaymentsByMonths()));
            }
            return result;
        }

        public IEnumerable<decimal> GetMonthlyPayments(TakenLoan takenLoan)
        {
            return this.GetMonthlyPayments(new Collection<TakenLoan> { takenLoan }).First().Value;
        }

        public CalculateCreditResult CalculateCredit(int loanProductId, decimal amount, int monthes)
        {
            var loanProduct = this.creditService.GetLoanProduct(loanProductId);
            var creditLogic = this.MapCreditPlanLogicEntityFromLoanProduct(
                monthes,
                amount,
                loanProduct.Percentage,
                loanProduct.Type);
            var result = new CalculateCreditResult
                             {
                                 Payments = creditLogic.GetPaymentsByMonths(),
                                 TotalPayment = creditLogic.GetTotalCreditPlanPayment()
                             };
            return result;
        }

        private BaseCreditPlanBusinessLogicEntity MapCreditPlanLogicEntityFromLoanProduct(TakenLoan loanProduct)
        {
            return this.MapCreditPlanLogicEntityFromLoanProduct(
                loanProduct.MaturityInMonth,
                loanProduct.Amount,
                loanProduct.ProductLoan.Percentage,
                loanProduct.ProductLoan.Type);
        }

        private BaseCreditPlanBusinessLogicEntity MapCreditPlanLogicEntityFromLoanProduct(
            int monthes,
            decimal amount,
            int percents,
            LoanProductType type)
        {
            switch (type)
            {
                case LoanProductType.Anuitet:
                    {
                        return new AnuitetCreditPlanBusinessLogicEntity(
                            monthes,
                            amount,
                            percents);
                    }
                case LoanProductType.Differential:
                    {
                        return new DifferentialCreditPlanBusinessLogicEntity
                                   {
                                       CreditPeriodInMonths = monthes,
                                       CreditSum = amount,
                                       Percents = percents
                                   };
                    }
                case LoanProductType.OneTimePayment:
                    {
                        return new OneTimePaymentCreditPlanBusinessLogicEntity
                                   {
                                       CreditPeriodInMonths = monthes,
                                       CreditSum = amount,
                                       Percents = percents
                                   };
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }
    }
}
