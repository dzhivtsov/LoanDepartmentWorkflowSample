namespace GangsterBank.Web.Models.EditEmailTemplate
{
    using System.Web.Mvc;

    using GangsterBank.Domain.Entities.EmailTemplates;

    public class EditEmailTemplateViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        public EmailTemplateType Type { get; set; }
    }
}