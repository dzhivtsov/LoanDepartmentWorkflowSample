namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using GangsterBank.Domain.Entities.EmailTemplates;

    public interface IEmailTemplatesRepository : IRepository<EmailTemplate>
    {
        EmailTemplate GetByType(EmailTemplateType type);
    }
}
