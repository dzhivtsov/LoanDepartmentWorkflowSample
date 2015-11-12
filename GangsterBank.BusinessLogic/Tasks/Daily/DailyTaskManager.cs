using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangsterBank.BusinessLogic.Tasks.Daily
{
    using System.Collections.ObjectModel;

    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;
    using GangsterBank.Core.Extensions;

    public class DailyTaskManager : IDailyTaskManager
    {
        private readonly IEnumerable<IOperationalDayTask> tasks;

        public DailyTaskManager(
            CalculateFineTask calculateFineTasktask,
            SendPaymentNotification sendPaymentNotification,
            PayLoanPaymentsTask payLoanPaymentsTask)
        {
            this.tasks = new Collection<IOperationalDayTask>
                             {
                                 calculateFineTasktask,
                                 sendPaymentNotification,
                                 payLoanPaymentsTask
                             };
        }

        public void Execute(IOperationalDayContext context)
        {
            this.tasks.ForEach(x => x.Execute(context));
        }
    }
}
