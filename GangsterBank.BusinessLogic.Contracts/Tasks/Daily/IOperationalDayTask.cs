namespace GangsterBank.BusinessLogic.Contracts.Tasks.Daily
{
    public interface IOperationalDayTask
    {
        void Execute(IOperationalDayContext context);
    }
}
