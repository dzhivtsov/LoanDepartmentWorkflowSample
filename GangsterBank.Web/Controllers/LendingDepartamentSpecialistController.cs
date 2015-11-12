using System.Web.Mvc;

namespace GangsterBank.Web.Controllers
{
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;

    [GangsterBankAuthorize(Role.LendingDepartmentHead, Role.Administrator, Role.LendingDepartmentSpecialist)]
    public class LendingDepartamentSpecialistController : Controller
    {
        //
        // GET: /LendingDepartamentSpecialist/
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult DraftCredits()
        {
            return View();
        }
	}
}