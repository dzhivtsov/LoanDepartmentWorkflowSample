using System.Web.Mvc;

namespace GangsterBank.Web.Controllers
{
    using GangsterBank.BusinessLogic.Contracts.EmailTemplates;
    using GangsterBank.Domain.Entities.EmailTemplates;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Models.EditEmailTemplate;

    [GangsterBankAuthorize(Role.LendingDepartmentHead, Role.Administrator, Role.LendingDepartmentSpecialist)]
    public class EmailTemplatesController : Controller
    {
        private readonly IEmailTemplatesService emailTemplatesService;

        public EmailTemplatesController(IEmailTemplatesService emailTemplatesService)
        {
            this.emailTemplatesService = emailTemplatesService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult RejectRequest()
        {
            var model = this.GetModel(EmailTemplateType.DeclineCreditRequest);
            return this.View("EditEmailTemplate", model);
        }

        public ActionResult ApproveRequest()
        {
            var model = this.GetModel(EmailTemplateType.ApproveCreditRequest);
            return this.View("EditEmailTemplate", model);
        }

        public ActionResult ApproveUser()
        {
            var model = this.GetModel(EmailTemplateType.ApproveUser);
            return this.View("EditEmailTemplate", model);
        }

        public ActionResult Deposit()
        {
            var model = this.GetModel(EmailTemplateType.Deposit);
            return this.View("EditEmailTemplate", model);
        }

        public ActionResult Withdraw()
        {
            var model = this.GetModel(EmailTemplateType.Withdraw);
            return this.View("EditEmailTemplate", model);
        }

        public ActionResult UpdateTemplate(EditEmailTemplateViewModel model)
        {
            var template = this.emailTemplatesService.GetByType(model.Type);
            template.Subject = model.Subject;
            template.Text = model.Text;
            template.Type = model.Type;
            this.emailTemplatesService.CreateOrUpdate(template);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private EditEmailTemplateViewModel GetModel(EmailTemplateType type)
        {
            var template = this.emailTemplatesService.GetByType(type);
            return new EditEmailTemplateViewModel
                       {
                           Id = template.Id,
                           Subject = template.Subject,
                           Text = template.Text,
                           Type = type
                       };
        }

	}
}