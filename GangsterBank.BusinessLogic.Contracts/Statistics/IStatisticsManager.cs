namespace GangsterBank.BusinessLogic.Contracts.Statistics
{
    using System.Collections.Generic;

    using GangsterBank.Domain.BusinessLogicEntities.Statistics;

    public interface IStatisticsManager
    {
        IEnumerable<SummaryLoanProductStatistic> GetSummaryLoanProductStatistics(IStatisticContext context);

        IEnumerable<TakenLoanPaymentStatistic> GetTakenLoanPaymentStatistics(IStatisticContext context);

        IEnumerable<PossiblePaymentsStatistic> GetPossiblePaymentsStatistics(IStatisticContext context);
    }
}
