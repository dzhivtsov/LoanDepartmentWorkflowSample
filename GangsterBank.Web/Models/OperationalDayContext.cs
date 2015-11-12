namespace GangsterBank.SchedulerService
{
    using System;

    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;

    public class OperationalDayContext : IOperationalDayContext
    {
        #region Constructors and Destructors

        public OperationalDayContext(DateTime currentDate)
        {
            this.CurrentDate = currentDate;
        }

        #endregion

        #region Public Properties

        public DateTime CurrentDate { get; private set; }

        #endregion
    }
}