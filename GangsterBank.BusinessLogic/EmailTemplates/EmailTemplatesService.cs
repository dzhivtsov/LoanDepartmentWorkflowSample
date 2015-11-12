namespace GangsterBank.BusinessLogic.EmailTemplates
{
    using GangsterBank.BusinessLogic.Contracts.EmailTemplates;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.EmailTemplates;

    public class EmailTemplatesService : IEmailTemplatesService
    {
        private readonly IEmailTemplatesRepository emailTemplatesRepository;

        public EmailTemplatesService(IEmailTemplatesRepository emailTemplatesRepository)
        {
            this.emailTemplatesRepository = emailTemplatesRepository;
        }

        public EmailTemplate GetByType(EmailTemplateType type)
        {
            var template = this.emailTemplatesRepository.GetByType(type);
            return template ?? new EmailTemplate();
        }

        public void CreateOrUpdate(EmailTemplate template)
        {
            this.emailTemplatesRepository.CreateOrUpdate(template);
        }
    }
}
