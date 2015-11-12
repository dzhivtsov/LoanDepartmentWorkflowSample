namespace GangsterBank.DataAccess.Repositories
{
    using System.Linq;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.EmailTemplates;

    public class EmailTemplatesRepository : BaseRepository<EmailTemplate>, IEmailTemplatesRepository
    {
        public EmailTemplatesRepository(GangsterBankContext context)
            : base(context)
        {
        }

        public EmailTemplate GetByType(EmailTemplateType type)
        {
            return DbSet.FirstOrDefault(x => x.Type == type);
        }
    }
}
