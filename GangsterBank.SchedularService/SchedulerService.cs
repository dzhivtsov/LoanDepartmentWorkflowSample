namespace GangsterBank.SchedulerService
{
    using System;
    using System.ServiceProcess;
    using System.Threading;
    using System.Transactions;

    using Autofac;

    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;

    public partial class SchedulerService : ServiceBase
    {
        #region Static Fields

        private static readonly TimeSpan TimeOfDay = new TimeSpan(3, 30, 0);

        #endregion

        #region Fields

        private IContainer container;

        private Timer wakeUpTimer;

        #endregion

        #region Constructors and Destructors

        public SchedulerService()
        {
            this.InitializeComponent();
            this.ServiceName = "GangsterBankSchedularService";
        }

        #endregion

        #region Methods

        protected override void OnStart(string[] args)
        {
            this.InitializeContainer();
            this.InitializeWakeUpTimer();
        }

        protected override void OnStop()
        {
            this.wakeUpTimer.Dispose();
            this.wakeUpTimer = null;
            this.container.Dispose();
        }

        private static DateTime ResolveFirstWakeUpTime(DateTime currentDateTime)
        {
            DateTime firstWakeUpTime = currentDateTime.Date.Add(TimeOfDay);
            if (firstWakeUpTime <= currentDateTime)
            {
                firstWakeUpTime = firstWakeUpTime.AddDays(1);
            }

            return firstWakeUpTime;
        }

        private void InitializeContainer()
        {
            var builder = new ContainerBuilder();
            ContainerConfig.RegisterTypes(builder);
            this.container = builder.Build();
        }

        private void InitializeWakeUpTimer()
        {
            DateTime currentDateTime = DateTime.UtcNow;
            DateTime firstWakeUpTime = ResolveFirstWakeUpTime(currentDateTime);

            this.wakeUpTimer = new Timer(
                this.TimerTick, 
                null, 
                firstWakeUpTime.Subtract(currentDateTime), 
                TimeSpan.FromDays(1));
        }

        private void TimerTick(object stateInfo = null)
        {
            using (this.container.BeginLifetimeScope())
            {
                var dailyTaskManager = this.container.Resolve<IDailyTaskManager>();

                var operationalDayContext = new OperationalDayContext(DateTime.UtcNow);
                using (var transaction = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    dailyTaskManager.Execute(operationalDayContext);
                    transaction.Complete();
                }
            }
        }

        #endregion
    }
}