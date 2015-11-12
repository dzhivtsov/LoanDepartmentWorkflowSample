namespace GangsterBank.Web.Models.ClientProfile
{
    using System;

    using global::Kendo.Mvc.UI;

    public class PaymentShedularModel : ISchedulerEvent
    {
        public int OwnerId { get; set; }

        public int TaskId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsAllDay { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }

        public string RecurrenceException { get; set; }
    }
}