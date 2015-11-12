namespace GangsterBank.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Accounts;
    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Infrastructure.Extensions;
    using GangsterBank.Web.Models.Cashier;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    [GangsterBankAuthorize(
        Role.LendingDepartmentSpecialist,
        Role.LendingDepartmentHead,
        Role.Operator,
        Role.Administrator,
        Role.Cashier)]
    public class CashierController : BaseController
    {
        private readonly IClientsService clientsService;

        private readonly IPaymentService paymentService;


        public CashierController(
            IUserContext userContext,
            IClientsService clientsService,
            IPaymentService paymentService)
            : base(userContext)
        {
            this.clientsService = clientsService;
            this.paymentService = paymentService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Payments()
        {
            return this.View();
        }

        public ActionResult GetPayments([DataSourceRequest] DataSourceRequest request)
        {
            var result = clientsService.GetAllConfirmedClients().Select(x => new ClientPaymentViewModel
                                                                                 {
                                                                                     ClientId = x.Id,
                                                                                     ClientFirstName = x.FirstName,
                                                                                     ClientLastName = x.LastName,
                                                                                     Founds = x.PrimaryAccount.Amount.ToGBString()
                                                                                 });
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Deposit(int clientId)
        {
            var model = new OperationModel { ClientId = clientId };
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Deposit(OperationModel model)
        {
            var client = this.clientsService.GetClient(model.ClientId);
            this.paymentService.Deposit(client, model.Amount);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Withdraw(int clientId)
        {
            var model = new OperationModel
                            {
                                ClientId = clientId,
                                CurrentAmount =
                                    this.clientsService.GetClient(clientId).PrimaryAccount.Amount
                            };
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Withdraw(OperationModel model)
        {
            var client = this.clientsService.GetClient(model.ClientId);
            this.paymentService.Withdraw(client, model.Amount);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}