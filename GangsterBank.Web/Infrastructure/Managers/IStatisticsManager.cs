namespace GangsterBank.Web.Infrastructure.Managers
{
    using System;
    using System.Collections.Generic;

    using GangsterBank.Web.Models.Statistics;

    public interface IStatisticsManager
    {
        IEnumerable<SummaryLoanProductStatisticViewModel> GetSummaryLoanProductStatistics(DateTime start, DateTime end);

        IEnumerable<TakenLoanPaymentStatisticViewModel> GetTakenLoanPaymentStatistics(DateTime start, DateTime end);

        IEnumerable<PossiblePaymentStatisticsViewModel> GetPossiblePaymentStatistics(DateTime start, DateTime end);
    }
}