using System.Web.Mvc;

namespace GangsterBank.Web.Controllers
{
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;

    [GangsterBankAuthorize(Role.LendingDepartmentHead, Role.Administrator)]
    public class LendingDepartamentHeadController : Controller
    {
        //
        // GET: /LendingDepartamentHead/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadyForReviewCredits()
        {
            return View();
        }
        public ActionResult DraftCredits()
        {
            return View();
        }
        public ActionResult ActiveCredits()
        {
            return View();
        }
        public ActionResult ArchiveCredits()
        {
            return View();
        }
	}
}