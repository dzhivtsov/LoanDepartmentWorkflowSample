namespace GangsterBank.BusinessLogic.Tasks.Daily
{
    using System;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;

    public class SendPaymentNotification : IOperationalDayTask
    {
        private readonly IClientsService clientsService;

        private readonly IMailService mailService;

        private const string Send3DayPaymentNotificationSubject = "3 days before next payment";

        private const string Send3DayPaymentNotificationBody = "Dear {0}, your next payment ({1}) should be done in 3 days";

        private const string SendTodayPaymentNotificationSubject = "Pay today!";

        private const string SendTodayPaymentNotificationBody = "Dear {0}, your next payment ({1}) is today!";

        private const string SendDelinquentPaymentNotificationSubject = "You missed payment...";

        private const string SendDelinquentPaymentNotificationBody = "Dear {0}, you missed your payment ({1}). Please pay, or we will find you";
        


        public SendPaymentNotification(
            IClientsService clientsService,
            IMailService mailService)
        {
            this.clientsService = clientsService;
            this.mailService = mailService;
        }

        public void Execute(IOperationalDayContext context)
        {
            this.clientsService.GetAllClients().ForEach(x => this.SendMessagesToUser(x, context.CurrentDate));
        }

        private void SendMessagesToUser(Client client, DateTime date)
        {
            client.TakenLoans.Where(x => x.Status == TakenLoanStatus.Active)
                .ForEach(x => this.ProceedTakenLoans(x, date, client));
        }

        private void ProceedTakenLoans(TakenLoan takenLoans, DateTime date, Client client)
        {
            takenLoans.Payments.ForEach(x => this.ProceedPayment(x, date, client));
        }

        private void ProceedPayment(LoanPayment payment, DateTime date, Client client)
        {
            var paymentDate = payment.Date;
            if (paymentDate < date)
            {
                this.SendDelinquentPaymentNotification(payment, client);
            }
            else if (paymentDate == date)
            {
                this.SendTodayPaymentNotification(payment, client);
            }
            else if (paymentDate < date.AddDays(3))
            {
                this.Send3DayPaymentNotification(payment, client);
            }
        }

        private void Send3DayPaymentNotification(LoanPayment payment, Client client)
        {
            mailService.SendMessage(
                Send3DayPaymentNotificationSubject,
                String.Format(Send3DayPaymentNotificationBody, client.FirstName, payment.Amount),
                client.Email);
        }

        private void SendTodayPaymentNotification(LoanPayment payment, Client client)
        {
            mailService.SendMessage(
                SendTodayPaymentNotificationSubject,
                String.Format(SendTodayPaymentNotificationBody, client.FirstName, payment.Amount),
                client.Email);
        }

        private void SendDelinquentPaymentNotification(LoanPayment payment, Client client)
        {
            mailService.SendMessage(
                SendDelinquentPaymentNotificationSubject,
                String.Format(SendDelinquentPaymentNotificationBody, client.FirstName, payment.Amount),
                client.Email);
        }
    }
}
