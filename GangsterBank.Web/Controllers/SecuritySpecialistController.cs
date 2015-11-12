using System.Web.Mvc;

namespace GangsterBank.Web.Controllers
{
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;

    [GangsterBankAuthorize(Role.SecuritySpecialist, Role.Administrator)]
    public class SecuritySpecialistController : Controller
    {
        //
        // GET: /SecuritySpecialist/
        public ActionResult Index()
        {
            return View();
        }
	}
}