namespace GangsterBank.BusinessLogic.Contracts.Tasks.Daily
{
    using System;

    public interface IOperationalDayContext
    {
        DateTime CurrentDate { get; }
    }
}
