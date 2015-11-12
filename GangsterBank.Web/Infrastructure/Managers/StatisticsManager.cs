namespace GangsterBank.Web.Infrastructure.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.BusinessLogic.Statistics;
    using GangsterBank.Web.Infrastructure.Extensions;
    using GangsterBank.Web.Models.Statistics;

    public class StatisticsManager : IStatisticsManager
    {
        private readonly BusinessLogic.Contracts.Statistics.IStatisticsManager statisticsManager;

        public StatisticsManager(BusinessLogic.Contracts.Statistics.IStatisticsManager statisticsManager)
        {
            this.statisticsManager = statisticsManager;
        }

        public IEnumerable<PossiblePaymentStatisticsViewModel> GetPossiblePaymentStatistics(
            DateTime start,
            DateTime end)
        {
            var context = this.CreateContext(start, end);
            var data = this.statisticsManager.GetPossiblePaymentsStatistics(context).ToArray();
            var allAmount = data.Select(x => x.TotalAmount).Sum();
            return data.Select(x => new PossiblePaymentStatisticsViewModel
                                        {
                                            CategoryName = x.LoanProductName,
                                            TotalAmount = x.TotalAmount.ToGBString(),
                                            Percentage = (double)(x.TotalAmount * 100 / allAmount)
                                        });
        }

        public IEnumerable<TakenLoanPaymentStatisticViewModel> GetTakenLoanPaymentStatistics(
            DateTime start,
            DateTime end)
        {
            var context = this.CreateContext(start, end);
            var data = this.statisticsManager.GetTakenLoanPaymentStatistics(context).ToArray();
            var totalAllCount = data.Select(x => x.TakeCount).Sum();
            // magic:)
            totalAllCount = totalAllCount == 0 ? 1 : totalAllCount;
            return data.Select(x => new TakenLoanPaymentStatisticViewModel
                                        {
                                            CategoryName = x.PaymentCategory,
                                            TakeCount = x.TakeCount,
                                            TotalAmount = x.TotalAmount.ToGBString(),
                                            Percentage = x.TakeCount * 100 / totalAllCount
                                        });
        }

        public IEnumerable<SummaryLoanProductStatisticViewModel> GetSummaryLoanProductStatistics(DateTime start, DateTime end)
        {
            var context = CreateContext(start, end);
            var data = this.statisticsManager.GetSummaryLoanProductStatistics(context).ToArray();
            var totalAllCount = data.Select(x => x.TakeCount).Sum();
            return data.Select(x => new SummaryLoanProductStatisticViewModel
                        {
                            CategoryName = x.LoanProductName,
                            Status = x.Status.ToString(),
                            TakeCount = x.TakeCount,
                            TotalAmount = x.TotalAmount.ToGBString(),
                            LoanProductId = x.LoanProductId,
                            Percentage = x.TakeCount * 100 / totalAllCount 
                        });
        }

        private StatisticContext CreateContext(DateTime start, DateTime end)
        {
            return new StatisticContext
                    {
                        End = end,
                        Start = start
                    };
        }
    }
}