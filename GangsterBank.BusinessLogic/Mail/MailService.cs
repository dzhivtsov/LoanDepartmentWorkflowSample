namespace GangsterBank.BusinessLogic.Mail
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.EmailTemplates;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.EmailTemplates;

    public class MailService : IMailService
    {
        private const string UserName = "bankgangsta@gmail.com";
        private const string Password = "bankgangsta12345";
        private const string FromName = "Gangsta Bank";
        private const string FromAddress = "bankgangsta@gmail.com";

        private readonly IEmailTemplatesService emailTemplatesService;

        public MailService(
            IEmailTemplatesService emailTemplatesService)
        
        {
            this.emailTemplatesService = emailTemplatesService;
        }

        public bool SendMessage(string subject, string body, IEnumerable<string> recepients)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password)
            };
            using (var message = new MailMessage
            {
                Subject = subject,
                Body = body,
            })
            {
                message.From = new MailAddress(FromAddress, FromName);
                message.IsBodyHtml = true;
                recepients.ForEach(x => message.To.Add(new MailAddress(x)));
                try
                {
                    smtp.Send(message);
                }
                catch (Exception)
                {
                    //TODO: shouild be logged
                    return false;
                }
            }
            return true;
        }

        public bool SendMessage(string subject, string body, string recepient)
        {
            return !string.IsNullOrEmpty(recepient) && this.SendMessage(subject, body, new Collection<string> { recepient });
        }

        public bool SendMessage(EmailTemplateType type, Client client, string text = null)
        {
            var template = this.emailTemplatesService.GetByType(type);
            var body = template.Text;
            var subject = template.Subject;
            if (!string.IsNullOrEmpty(text))
            {
                body = body.Replace("{text}", text);
            }
            subject = subject.Replace("{firstname}", client.FirstName);
            body = this.MassReplace(
                body,
                new Collection<KeyValuePair<string, string>> { 
                    new KeyValuePair<string, string>("{firstname}", client.FirstName),
                    new KeyValuePair<string, string>("{lastname}", client.LastName),
                    new KeyValuePair<string, string>("{time}", DateTime.Now.ToShortTimeString()),
                    new KeyValuePair<string, string>("{date}", DateTime.Now.ToShortDateString()),
                    new KeyValuePair<string, string>("{currentamount}", client.PrimaryAccount.Amount.ToString("C0"))
                });
            subject = subject.Replace("{lastname}", client.FirstName);
            return this.SendMessage(subject, body, client.Email);
        }

        public bool SendMessage(EmailTemplateType type, Client client, LoanRequest loanRequest, string text = null)
        {
            var template = this.emailTemplatesService.GetByType(type);
            var body = template.Text;
            var subject = template.Subject;
            if (!string.IsNullOrEmpty(text))
            {
                body = body.Replace("{text}", text);
            }
            subject = subject.Replace("{firstname}", client.FirstName);
            body = this.MassReplace(
                body,
                new Collection<KeyValuePair<string, string>> { 
                    new KeyValuePair<string, string>("{firstname}", client.FirstName),
                    new KeyValuePair<string, string>("{lastname}", client.LastName),
                    new KeyValuePair<string, string>("{time}", DateTime.Now.ToShortTimeString()),
                    new KeyValuePair<string, string>("{date}", DateTime.Now.ToShortDateString()),
                    new KeyValuePair<string, string>("{currentamount}", client.PrimaryAccount.Amount.ToString("C0"))
                });
            body = body.Replace("{amount}", loanRequest.Amount.ToString());
            body = body.Replace("{productname}", loanRequest.LoanProduct.Name);
            subject = subject.Replace("{lastname}", client.FirstName);
            return this.SendMessage(subject, body, client.Email);
        }

        private string MassReplace(string target, IEnumerable<KeyValuePair<string, string>> replacRules)
        {
            return replacRules.Aggregate(target, (current, keyValuePair) => current.Replace(keyValuePair.Key, keyValuePair.Value));
        }
    }
}
