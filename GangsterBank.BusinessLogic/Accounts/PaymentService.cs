namespace GangsterBank.BusinessLogic.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Accounts;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.EmailTemplates;

    public class PaymentService : IPaymentService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        private readonly IMailService mailService;

        #endregion

        #region Constructors and Destructors

        public PaymentService(
            IGangsterBankUnitOfWork gangsterBankUnitOfWork,
            IMailService mailService)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork.IsNotNull());
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
            this.mailService = mailService;
        }

        #endregion

        #region Public Methods and Operators

        public void Deposit(Client client, decimal amount)
        {
            Contract.Requires<ArgumentNullException>(client.IsNotNull());
            Contract.Requires<ArgumentOutOfRangeException>(amount > 0);

            this.CreateNewPayment(client, amount, PaymentType.Debit);
            amount = this.PayLoanPayments(client, amount);
            CheckTakenLoansStatus(client);
            client.PrimaryAccount.Amount += amount;
            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(client);
            this.mailService.SendMessage(EmailTemplateType.Deposit, client, amount.ToString("C0"));
        }

        public void TryPayLoanPayment(LoanPayment loanPayment)
        {
            Contract.Requires<ArgumentNullException>(loanPayment.IsNotNull());
            Account account = loanPayment.TakenLoan.Client.PrimaryAccount;
            account.Amount = TryPayLoanPayment(account.Amount, loanPayment);
            CheckLoanPaymentStatus(loanPayment);
            CheckTakenLoanStatus(loanPayment.TakenLoan);
            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(loanPayment.TakenLoan.Client);
        }

        public IEnumerable<Payment> GetAllPaymentsForClient(int clientId)
        {
            return this.gangsterBankUnitOfWork.PaymentsRepository.GetAll().Where(x => x.Account.ClientId == clientId)
                .OrderByDescending(x => x.DateTime);
        }

        public void Withdraw(Client client, decimal amount)
        {
            Contract.Requires<ArgumentNullException>(client.IsNotNull());
            Contract.Requires<ArgumentOutOfRangeException>(amount > 0);

            if (client.PrimaryAccount.Amount < amount)
            {
                throw new ApplicationException("Not enough money");
            }

            this.CreateNewPayment(client, amount, PaymentType.Credit);
            client.PrimaryAccount.Amount -= amount;
            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(client);
            this.mailService.SendMessage(EmailTemplateType.Withdraw, client, amount.ToString("C0"));
        }

        #endregion

        #region Methods

        private static void CheckLoanPaymentStatus(LoanPayment loanPayment)
        {
            if (loanPayment.Fine == 0 && loanPayment.Amount == 0)
            {
                loanPayment.Status = loanPayment.Date.Date < DateTime.Now.Date
                                         ? LoanPaymentStatus.PaidWithDelay
                                         : LoanPaymentStatus.Paid;
            }
        }

        private static void CheckTakenLoanStatus(TakenLoan takenLoan)
        {
            if (takenLoan.Payments.All(loanPayment => loanPayment.Status == LoanPaymentStatus.Paid || loanPayment.Status == LoanPaymentStatus.PaidWithDelay))
            {
                takenLoan.Status = TakenLoanStatus.Paid;
            }
        }

        private static void CheckTakenLoansStatus(Client client)
        {
            foreach (
                TakenLoan takenLoan in client.TakenLoans.Where(takenLoan => takenLoan.Status == TakenLoanStatus.Active))
            {
                CheckTakenLoanStatus(takenLoan);
            }
        }

        private static IEnumerable<LoanPayment> GetLoanPaymentsForPayment(Client client)
        {
            IEnumerable<LoanPayment> loanPayments = from takenLoan in client.TakenLoans
                                                    where takenLoan.Status == TakenLoanStatus.Active
                                                    from loanPayment in takenLoan.Payments
                                                    where loanPayment.Status == LoanPaymentStatus.Active
                                                    orderby loanPayment.Date
                                                    select loanPayment;
            return loanPayments;
        }

        private static decimal PayRequiredPaymentAmount(
            decimal amount, 
            decimal requiredPaymentAmount, 
            Action<decimal> paymentAmountMutator)
        {
            decimal paymentAmount = Math.Min(requiredPaymentAmount, amount);
            paymentAmountMutator(requiredPaymentAmount - paymentAmount);
            amount -= paymentAmount;
            return amount;
        }

        private static decimal TryPayLoanPayment(decimal amount, LoanPayment loanPayment)
        {
            amount = PayRequiredPaymentAmount(amount, loanPayment.Fine, newAmount => loanPayment.Fine = newAmount);
            amount = PayRequiredPaymentAmount(amount, loanPayment.Amount, newAmount => loanPayment.Amount = newAmount);

            return amount;
        }

        private void CreateNewPayment(Client client, decimal amount, PaymentType type)
        {
            var payment = new Payment
                              {
                                  Account = client.PrimaryAccount, 
                                  Amount = amount, 
                                  DateTime = DateTime.UtcNow, 
                                  Type = type
                              };
            this.gangsterBankUnitOfWork.PaymentsRepository.CreateOrUpdate(payment);
        }

        private decimal PayLoanPayments(Client client, decimal amount)
        {
            IEnumerable<LoanPayment> loanPayments = GetLoanPaymentsForPayment(client);
            foreach (LoanPayment loanPayment in loanPayments)
            {
                amount = TryPayLoanPayment(amount, loanPayment);
                CheckLoanPaymentStatus(loanPayment);
                if (amount == 0)
                {
                    break;
                }
            }

            return amount;
        }

        #endregion
    }
}