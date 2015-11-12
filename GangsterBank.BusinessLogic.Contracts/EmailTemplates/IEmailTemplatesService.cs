namespace GangsterBank.BusinessLogic.Contracts.EmailTemplates
{
    using GangsterBank.Domain.Entities.EmailTemplates;

    public interface IEmailTemplatesService
    {
        EmailTemplate GetByType(EmailTemplateType type);

        void CreateOrUpdate(EmailTemplate template);
    }
}
