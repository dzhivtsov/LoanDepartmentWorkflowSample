namespace GangsterBank.Domain.Entities.EmailTemplates
{
    using GangsterBank.Domain.Entities.Base;

    public class EmailTemplate: BaseEntity
    {
        public string Subject { get; set; }

        public string Text { get; set; }

        public EmailTemplateType Type { get; set; }
    }
}
