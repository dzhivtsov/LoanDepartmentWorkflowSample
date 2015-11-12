namespace GangsterBank.BusinessLogic.Contracts.Mail
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.EmailTemplates;

    public interface IMailService
    {
        bool SendMessage(string subject, string body, IEnumerable<string> recepients);

        bool SendMessage(string subject, string body, string recepient);

        bool SendMessage(EmailTemplateType type, Client client, string text = null);

        bool SendMessage(EmailTemplateType type, Client client, LoanRequest loanRequest, string text = null);
    }
}
