namespace GangsterBank.BusinessLogic.Contracts.Tasks.Daily
{
    public interface IDailyTaskManager
    {
        void Execute(IOperationalDayContext context);
    }
}
