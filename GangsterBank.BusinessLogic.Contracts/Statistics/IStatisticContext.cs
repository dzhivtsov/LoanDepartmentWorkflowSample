namespace GangsterBank.BusinessLogic.Contracts.Statistics
{
    using System;

    public interface IStatisticContext
    {
        DateTime Start { get; set; }

        DateTime End { get; set; }

        DateTime Today { get; set; }
    }
}
