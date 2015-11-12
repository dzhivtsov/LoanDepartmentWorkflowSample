namespace GangsterBank.BusinessLogic.Contracts.Accounts
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;

    public interface IPaymentService
    {
        void Deposit(Client client, decimal amount);

        void Withdraw(Client client, decimal amount);

        void TryPayLoanPayment(LoanPayment loanPayment);

        IEnumerable<Payment> GetAllPaymentsForClient(int clientId);
    }
}