namespace GangsterBank.BusinessLogic.Statistics
{
    using System;

    using GangsterBank.BusinessLogic.Contracts.Statistics;

    public class StatisticContext : IStatisticContext
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
            set
            {

            }
        }
    }
}
