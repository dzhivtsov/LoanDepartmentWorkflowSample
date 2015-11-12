namespace GangsterBank.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Accounts;
    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Repositories;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Infrastructure.Extensions;
    using GangsterBank.Web.Infrastructure.Managers;
    using GangsterBank.Web.Models;
    using GangsterBank.Web.Models.ClientProfile;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    [Authorize]
    public class ClientProfileController : BaseController
    {
        #region Fields

        private readonly IClientProfileManager clientProfileManager;

        private readonly IClientProfileService clientProfileService;

        private readonly ICreditManager creditManager;

        private readonly IPaymentService paymentService;

        #endregion

        #region Constructors and Destructors

        public ClientProfileController(
            IClientProfileService clientProfileService,
            IClientProfileManager clientProfileManager,
            IUserContext userContext,
            ICreditManager creditManager,
            IPaymentService paymentService)
            : base(userContext)
        {
            Contract.Requires<ArgumentNullException>(clientProfileService.IsNotNull());
            Contract.Requires<ArgumentNullException>(clientProfileManager.IsNotNull());

            this.clientProfileService = clientProfileService;
            this.clientProfileManager = clientProfileManager;
            this.creditManager = creditManager;
            this.paymentService = paymentService;
        }

        #endregion

        #region Public Methods and Operators


        public ActionResult Menu(int? clientId)
        {
            var model = new ClientMenuModel();
            model.BasicDetailsUrl = Url.Action("BasicDetails");
            model.PassportUrl = Url.Action("Passport");
            model.ContactsUrl = Url.Action("Contacts");
            model.EmploymentUrl = Url.Action("Employment");
            model.ObligationsUrl = Url.Action("Obligations");
            model.PropertyUrl = Url.Action("Property");
            model.CreditsUrl = Url.Action("Index");
            
            int id = this.ResolveClientId(clientId);
            string clientSubStr = @"?clientId=" + id;
            model.IsClient = _userContext.IsClient;
            
            if (id != _userContext.User.Id)
            {
                model.BasicDetailsUrl += clientSubStr;
                model.PassportUrl += clientSubStr;
                model.ContactsUrl += clientSubStr;
                model.EmploymentUrl += clientSubStr;
                model.ObligationsUrl += clientSubStr;
                model.PropertyUrl += clientSubStr;
                model.CreditsUrl += clientSubStr;
                model.IsClient = true;
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult BasicDetails(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            BasicDetailsViewModel model = this.GetBasicDetailsViewModel(resolvedClientId);
            return this.View(model);
        }

        [HttpPost]
        public ActionResult BasicDetails(BasicDetailsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.ClientId = this.ResolveClientId(model.ClientId);
            this.clientProfileManager.SaveBasicDetails(model);
            return this.RedirectToAction("Passport", new { clientId = model.ClientId });
        }

        public ActionResult Payments(int? clientId)
        {
            ViewBag.ClientId = clientId;
            return this.View();
        }

        public ActionResult PaymentsData([DataSourceRequest] DataSourceRequest request, int? clientId)
        {
            var id = this.ResolveClientId(clientId);
            var data = this.paymentService.GetAllPaymentsForClient(id).ToList();
            data.ForEach(x => x.Account = null);
            return this.Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClientCreditHistory(int? clientId)
        {
            ViewBag.ClientId = clientId;
            return this.View();
        }

        public ActionResult ClientCredits(int? clientId)
        {
            var resolvedClientId = this.ResolveClientId(clientId);
            this.ViewBag.ClientId = resolvedClientId;
            return this.View();
        }

        [HttpGet]
        public ActionResult Contacts(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            ContactsViewModel model = this.clientProfileManager.GetContactsViewModel(resolvedClientId);
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Contacts(ContactsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.ClientId = this.ResolveClientId(model.ClientId);
            this.clientProfileManager.SaveContacts(model);
            return this.RedirectToAction("Employment", new { clientId = model.ClientId });
        }

        [HttpGet]
        public ActionResult Employment(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            EmploymentViewModel model = this.clientProfileManager.GetEmploymentViewModel(resolvedClientId);
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Employment(EmploymentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.ClientId = this.ResolveClientId(model.ClientId);
            this.clientProfileManager.SaveEmployment(model);
            return this.RedirectToAction("Obligations", new { clientId = model.ClientId });
        }

        /// <summary>
        /// Returns client history(all completed credits)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetClientCreditHistory([DataSourceRequest] DataSourceRequest request, int clientId)
        {
            return
                this.Json(
                    this.clientProfileService.GetClient(clientId)
                        .TakenLoans.Where(x => x.Status == TakenLoanStatus.Paid)
                        .Select(x => new ClientCreditHistoryModel
                                               {
                                                   CreditPlanName = x.ProductLoan.Name,
                                                   CompletedOnTime = x.Payments.OrderByDescending(p => p.Date)
                                                        .First().Status == LoanPaymentStatus.Paid
                                               }).ToDataSourceResult(request),
                    JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns list of client credits
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult GetClientCredits([DataSourceRequest] DataSourceRequest request, int clientId)
        {
            return
                this.Json(
                    GetAllClientCredits(clientId).Select(x => new ClientCreditModel(x))
                        .ToDataSourceResult(request),
                    JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Index(int? clientId, int? id)
        {
            var resolvedId = this.ResolveClientId(clientId);
            if ((resolvedId == _userContext.User.Id) && !_userContext.IsClient)
            {
                return RedirectToAction("Dashboard","Account");
            }
            ViewBag.SelectedIndex = id ?? 0;

            return this.View(resolvedId);
        }

        public ActionResult Funds(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            Client client = this.clientProfileService.GetClient(resolvedClientId);
            return this.PartialView(client.PrimaryAccount.Amount);
        }
            
        [HttpGet]
        public ActionResult Obligations(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            
            return this.View(resolvedClientId);
        }

        [HttpPost]
        public ActionResult Obligations(ObligationsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.ClientId = this.ResolveClientId(model.ClientId);
            this.clientProfileManager.SaveObligations(model);
            return this.RedirectToAction("Property");
        }

        public ActionResult ClientObligations([DataSourceRequest] DataSourceRequest request, int? clientId)
        {
            Contract.Requires<ArgumentNullException>(request.IsNotNull());
            int resolvedClientId = this.ResolveClientId(clientId);
            ObligationsViewModel model = this.clientProfileManager.GetObligationsViewModel(resolvedClientId);
            ObligationViewModel[] properies = model.Obligations.ToArray();

            return Json(properies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddObligation(int? clientId, int? obligationId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            ObligationViewModel model;
           
            if (obligationId.HasValue)
            {
                model = clientProfileManager.GetObligation(resolvedClientId, obligationId.Value);
            }
            else
            {
                model = new ObligationViewModel { ClientId = resolvedClientId };
            }

            return this.View(model);
        }


        [HttpPost]
        public ActionResult AddObligation(ObligationViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            clientProfileManager.SaveObligation(model);

            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult RemObligation(int obligationId)
        {
            clientProfileManager.RemoveObligation(obligationId);
            return Json(true);
        }

        [HttpGet]
        public ActionResult Passport(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            PassportViewModel model = this.clientProfileManager.GetPassportViewModel(resolvedClientId);
            return this.View(model);
        }

        public ActionResult PassportView(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            PassportViewModel model = this.clientProfileManager.GetPassportViewModel(resolvedClientId);
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Passport(PassportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            var allowImageMimeTypes = new List<string>
                                                   {
                                                       "image/gif",
                                                       "image/jpeg",
                                                       "image/pjpeg",
                                                       "image/png",
                                                       "image/svg+xml",
                                                       "image/tiff",
                                                       "image/vnd.microsoft.icon",
                                                       "image/vnd.wap.wbmp"
                                                   };
            
            if ((model.File != null)&&(!allowImageMimeTypes.Contains(model.File.ContentType)))
            {
                ModelState.AddModelError("", "Unknown File Type!");
                return this.View(model);
            }

            model.ClientId = this.ResolveClientId(model.ClientId);
            this.clientProfileManager.SavePassportData(model, model.File);
            return this.RedirectToAction("Contacts");
        }

        public ActionResult PassportDigitalCopy(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            
            byte[] digitalCopy = this.clientProfileManager.GetClientDigitalCopy(resolvedClientId);

            return File(digitalCopy, "image/png");
        }

        [HttpGet]
        public ActionResult Property(int? clientId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);

            return this.View(resolvedClientId);
        }

        public ActionResult GetClientProperties([DataSourceRequest] DataSourceRequest request, int? clientId)
        {
            Contract.Requires<ArgumentNullException>(request.IsNotNull());
            int resolvedClientId = this.ResolveClientId(clientId);
            PropertiesViewModel model = this.clientProfileManager.GetPropertiesViewModel(resolvedClientId);
            PropertyViewModel[] properies = model.Properties.ToArray();

            return Json(properies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public ActionResult AddProperty(int? clientId, int? propertyId)
        {
            int resolvedClientId = this.ResolveClientId(clientId);
            PropertyViewModel model;
            if (propertyId.HasValue)
            {
                model = clientProfileManager.GetProperty(resolvedClientId, propertyId.Value);
            }
            else
            {
                model = new PropertyViewModel { ClientId = resolvedClientId };
            }

            return this.View(model);
        }

        
        [HttpPost]
        public ActionResult AddProperty(PropertyViewModel model)
        {
            if (!ModelState.IsValid) return this.View(model);

            clientProfileManager.SaveProperty(model);
            
            return Json(true, JsonRequestBehavior.DenyGet);
        }


        public ActionResult RemProperty(int propertyId)
        {
            clientProfileManager.RemoveProperty(propertyId);
            return Json(true);
        }

        public ActionResult PaymentCalendar(int? clientId)
        {
            ViewBag.ClientId = this.ResolveClientId(clientId);
            return this.View();
        }

        public JsonResult GetPaymentsForShedular([DataSourceRequest] DataSourceRequest request, int? clientId)
        {
            var result = this.GetPayments(request, clientId);
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Methods

        private BasicDetailsViewModel GetBasicDetailsViewModel(int clientId)
        {
            Client client = this.clientProfileService.GetClient(clientId);
            var model = new BasicDetailsViewModel
                            {
                                ClientId = client.Id,
                                FirstName = client.FirstName,
                                LastName = client.LastName,
                                BirthDate = client.PersonalDetails.BirthDate,
                                Gender = client.PersonalDetails.Gender,
                                Email = client.Email
                            };
            return model;
        }

        private IEnumerable<PaymentShedularModel> GetPayments(DataSourceRequest request, int? clientId)
        {
            var resolvedClientId = this.ResolveClientId(clientId);
            var allActiveCredits = this.GetAllClientCredits(resolvedClientId);
            //var calculatedData = this.creditManager.GetMonthlyPayments(allActiveCredits);
            var result = this.GetCreditPaymentsToShow(allActiveCredits);
            return result;
        }

        private IEnumerable<PaymentShedularModel> GetCreditPaymentsToShow(IEnumerable<TakenLoan> data)
        {
            var result = new List<PaymentShedularModel>();
            foreach (var credit in data)
            {
                var takenCredit = credit;
                var currentDate = DateTime.Now;
                foreach (var payment in credit.Payments.Where(x => x.Status == LoanPaymentStatus.Active))
                {
                    var paymentDate = payment.Date;
                    if (paymentDate > currentDate)
                    {
                        if (payment.Fine != 0)
                        {
                            result.Add(
                                this.CreatePaymentShedularModel(
                                    String.Format(
                                        "{0}: {1}; Fine is {2}; Total: {3}",
                                        takenCredit.ProductLoan.Name,
                                        payment.Amount.ToGBString(),
                                        payment.Fine.ToGBString(),
                                        (payment.Amount + payment.Fine).ToGBString()),
                                    paymentDate));
                        }
                        else
                        {
                            result.Add(
                                this.CreatePaymentShedularModel(
                                    String.Format(
                                        "{0}: {1}",
                                        takenCredit.ProductLoan.Name,
                                        Math.Ceiling(payment.Amount)),
                                    paymentDate));
                        }
                    }
                }
            }
            return result;
        }

        private PaymentShedularModel CreatePaymentShedularModel(string title, DateTime start)
        {
            return new PaymentShedularModel
                {
                    Description = title,
                    IsAllDay = true,
                    TaskId = 0,
                    OwnerId = 1,
                    Title = title,
                    Start = start,
                    End = start
                };
        }

        private IEnumerable<TakenLoan> GetAllClientCredits(int clientId)
        {
            return
                this.clientProfileService.GetClient(clientId)
                    .TakenLoans.Where(takenLoan=>takenLoan.Status == TakenLoanStatus.Active);
        }

        #endregion
    }
}