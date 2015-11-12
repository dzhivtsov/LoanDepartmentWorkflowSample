using System.Web.Mvc;

namespace GangsterBank.Web.Controllers
{
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Models.Clients;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    [GangsterBankAuthorize(Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead, Role.Operator, Role.Administrator)]
    public class ClientsController : Controller
    {
        private readonly IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        //
        // GET: /Clients/
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ApprovedClients()
        {
            return this.View("Clients/ApprovedClients");
        }

        public ActionResult GetApprovedClients([DataSourceRequest] DataSourceRequest request)
        {
            var clients = this.clientsService.GetAllConfirmedClients().Select(x => new ConfirmedClientViewModel
                                                                                       {
                                                                                           FirstName = x.FirstName,
                                                                                           LastName = x.LastName,
                                                                                           Id = x.Id,
                                                                                           PassportNumber = x.PersonalDetails.PassportData.PassportNumber,
                                                                                           PersonalNumber = x.PersonalDetails.PassportData.PersonalNumber
                                                                                       });
            return this.Json(clients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
	}
}