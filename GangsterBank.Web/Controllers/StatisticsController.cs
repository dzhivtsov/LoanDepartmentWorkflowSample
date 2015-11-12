namespace GangsterBank.Web.Controllers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Infrastructure.Extensions;
    using GangsterBank.Web.Infrastructure.Managers;
    using GangsterBank.Web.Models.Statistics;

    using Kendo.Mvc.UI;

    [GangsterBankAuthorize(
        Role.LendingDepartmentSpecialist,
        Role.LendingDepartmentHead,
        Role.Administrator)]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsManager statisticsManager;

        private readonly IClientsService clientsService;

        public StatisticsController(
            IStatisticsManager statisticsManager,
            IClientsService clientsService)
        {
            this.statisticsManager = statisticsManager;
            this.clientsService = clientsService;
        }

        //
        // GET: /Statictics/
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult StatisticsTab()
        {
            return this.View();
        }

        public ActionResult SummaryLoanProductStatistics()
        {
            return this.View();
        }

        public ActionResult SummaryLoanProductStatisticsData()
        {
            return this.Json(
                this.statisticsManager.GetSummaryLoanProductStatistics(new DateTime(), DateTime.Today.AddYears(11)),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult TakenLoanPaymentStatistics()
        {
            return this.View();
        }

        public ActionResult TakenLoanPaymentStatisticsData()
        {
            return this.Json(
                this.statisticsManager.GetTakenLoanPaymentStatistics(new DateTime(), DateTime.Today.AddYears(11)),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Debtors()
        {
            return this.View();
        }

        public ActionResult GetDebtors([DataSourceRequest] DataSourceRequest request)
        {
            var clients = this.clientsService.GetAllConfirmedClients().ToArray();
            var result = new Collection<DebtorViewModel>();
            foreach (var client in clients)
            {
                var takenLoans = client.TakenLoans;
                foreach (var takenLoan in takenLoans)
                {
                    var missedPayments =
                        takenLoan.Payments.Where(x => x.Date < DateTime.Today && x.Status == LoanPaymentStatus.Active)
                        .ToArray();
                    if (missedPayments.Any())
                    {
                        result.Add(new DebtorViewModel
                                       {
                                           FirstName = client.FirstName,
                                           LastName = client.LastName,
                                           LoanProductName = takenLoan.ProductLoan.Name,
                                           Debt = missedPayments.Select(x => x.Amount).Sum().ToGBString()
                                       });
                    }
                }
            }
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PossiblePaymentsStatistics()
        {
            return this.View();
        }

        public ActionResult PossiblePaymentsStatisticsData()
        {
            return this.Json(
                this.statisticsManager.GetPossiblePaymentStatistics(new DateTime(), DateTime.Today.AddYears(11)),
                JsonRequestBehavior.AllowGet);
        }
	}
}