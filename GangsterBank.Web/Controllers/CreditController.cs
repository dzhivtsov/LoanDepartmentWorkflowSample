namespace GangsterBank.Web.Controllers
{
    #region

    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.EmailTemplates;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.BusinessLogic.Contracts.Membership;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.EmailTemplates;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Domain.Workflow;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Infrastructure.Extensions;
    using GangsterBank.Web.Models.Credit;
    using GangsterBank.Web.Models.Credit.CalculateCredit;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using WebGrease.Css.Extensions;

    using ListExtensions = WebGrease.Css.Extensions.ListExtensions;

    #endregion

    public class CreditController : BaseController
    {
        #region Fields

        private readonly IClientProfileService _clientProfileService;

        private readonly ICreditService _creditService;

        private readonly ICreditManager creditManager;

        private readonly ILoanRequestsService loanRequestsService;

        private readonly IUserService userService;

        private readonly IMailService mailService;

        private readonly IEmailTemplatesService emailTemplatesService;

        #endregion

        #region Constructors and Destructors

        public CreditController(
            ICreditService creditService, 
            IUserContext userContext, 
            IClientProfileService clientProfileService, 
            IUserService userService, 
            ILoanRequestsService loanRequestsService, 
            ICreditManager creditManager, IMailService mailService, IEmailTemplatesService emailTemplatesService)
            : base(userContext)
        {
            Contract.Requires<ArgumentNullException>(creditService.IsNotNull());
            this._creditService = creditService;
            this._clientProfileService = clientProfileService;
            this.userService = userService;
            this.loanRequestsService = loanRequestsService;
            this.creditManager = creditManager;
            this.mailService = mailService;
            this.emailTemplatesService = emailTemplatesService;
        }

        #endregion

        #region Public Methods and Operators

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead, 
            Role.Operator, Role.SecuritySpecialist)]
        public ActionResult ApprovalRequests()
        {
            return this.View("CreditRequests/ApprovalRequests");
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentHead)]
        public ActionResult ApproveCredit(int creditId)
        {
            return this.ChangeCreditStatus(creditId, LoanProductStatus.Active);
        }

        public ActionResult ApproveRequest(int id)
        {
            this.loanRequestsService.ApproveLoanRequest(id, this._userContext);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentHead)]
        public ActionResult ArchiveCredit(int creditId)
        {
            return this.ChangeCreditStatus(creditId, LoanProductStatus.Archived);
        }

        // GET: /Credit/
        public ActionResult AvailableCredits()
        {
            return this.View("AvailableCredits/AvailableCredits");
        }

        public ActionResult CalculateCredit(CalculateCreditModel creditModel)
        {
            return this.GetCalculateCreditResult(creditModel);
        }

        public ActionResult ConcreteCreditCalculator(int loanProductId)
        {
            var model = this.FillConcreteCreditCalculatorModel(loanProductId);
            return this.View("CreditCalulator/ConcreteCreditCalculator", model);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead)]
        [HttpGet]
        public ActionResult CreateCredit()
        {
            return this.View(
                "CreateCredit", 
                new LoanProductViewModel
                    {
                        Requirements =
                            new LoanProductRequirementsViewModel
                                {
                                    Approvers =
                                        Enumerable.Empty<Role>()
                                }
                    });
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead)]
        [HttpPost]
        public ActionResult CreateCredit(LoanProductViewModel model)
        {
            Contract.Requires<ArgumentNullException>(model.IsNotNull());
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("CreateCreditFields", model);
            }

            var loanProduct = MapLoanProduct(model, new LoanProduct { Requirements = new LoanProductRequirements() });
            

            var result = this._creditService.CreateLoanProduct(loanProduct);
            return this.Json(result);
        }

        private LoanProduct MapLoanProduct(LoanProductViewModel model, LoanProduct loanProduct)
        {
            loanProduct.Id = model.LoanProductId;
            loanProduct.MinAmount = model.MinAmount;
            loanProduct.MaxAmount = model.MaxAmount;
            loanProduct.Percentage = model.Percentage;
            loanProduct.MinPeriodInMonth = model.MinPeriodInMonth;
            loanProduct.MaxPeriodInMonth = model.MaxPeriodInMonth;
            loanProduct.Name = model.Name;
            loanProduct.Description = model.Description;
            loanProduct.Type = model.Type;
            loanProduct.Status = LoanProductStatus.Draft;
            loanProduct.FineDayPercentage = model.FineDayPercentage;
            loanProduct.AdvancedRepaymentFirstPossibleMonth = model.AdvancedRepaymentFirstPossibleMonth;
            loanProduct.AdvancedRepaymentFinePercentage = model.AdvancedRepaymentFinePercentage;
            loanProduct.Requirements.Id = model.Requirements.LoanProductRequirementsId;
            loanProduct.Requirements.MinWorkOnLastJobInMonths = model.Requirements.MinWorkOnLastJobInMonths;
            loanProduct.Requirements.MinSalary = model.Requirements.MinSalary;
            loanProduct.Requirements.NeedEarningsRecord = model.Requirements.NeedEarningsRecord;
            loanProduct.Requirements.NeedGuarantors = model.Requirements.NeedGuarantors;
            loanProduct.Requirements.GuarantorsCount = model.Requirements.GuarantorsCount;
            loanProduct.Requirements.Approvers =
                this.userService.GetRoles()
                    .Where(x => model.Requirements.Approvers.Select(approver => approver.ToString()).Contains(x.Name))
                    .ToList();
            return loanProduct;
        }

        [HttpGet]
        public ActionResult CreditCalculator()
        {
            var model = this.FillCreditCalculatorModel();
            return this.View("CreditCalulator/CreditCalculator", model);
        }

        [HttpGet]
        public ActionResult DeclineRequest(int id, int clientId)
        {
            //this.loanRequestsService.DeclineLoanRequest(id, this._userContext);
            var model = new DeclineRequestViewModel
                            {
                                Id = id,
                                ClientId = clientId
                            };
            return this.View("CreditRequests/DeclineRequest", model);
        }

        [HttpPost]
        public ActionResult DeclineRequest(DeclineRequestViewModel model)
        {
            this.loanRequestsService.DeclineLoanRequest(model.Id, this._userContext);
            var client = this._clientProfileService.GetClient(model.ClientId);
            this.mailService.SendMessage(EmailTemplateType.DeclineCreditRequest, client, model.Text);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentHead)]
        public ActionResult DraftCredit(int creditId)
        {
            return this.ChangeCreditStatus(creditId, LoanProductStatus.Draft);
        }

        public ActionResult ViewCredit(int creditId)
        {
            var loanProduct = this._creditService.GetLoanProduct(creditId);
            var model = this.GetModelFromLoanProduct(loanProduct);
            return this.View("CreateCreditFieldsForView", model);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead)]
        [HttpGet]
        public ActionResult EditCredit(int creditId)
        {
            var loanProduct = this._creditService.GetLoanProduct(creditId);
            var model = this.GetModelFromLoanProduct(loanProduct);
            return this.View("CreateCredit", model);
        }

        private LoanProductViewModel GetModelFromLoanProduct(LoanProduct loanProduct)
        {
            var model = new LoanProductViewModel
            {
                MinAmount = loanProduct.MinAmount,
                MaxAmount = loanProduct.MaxAmount,
                Percentage = loanProduct.Percentage,
                MinPeriodInMonth = loanProduct.MinPeriodInMonth,
                MaxPeriodInMonth = loanProduct.MaxPeriodInMonth,
                Name = loanProduct.Name,
                Description = loanProduct.Description,
                Type = loanProduct.Type,
                FineDayPercentage = loanProduct.FineDayPercentage,
                AdvancedRepaymentFirstPossibleMonth =
                    loanProduct.AdvancedRepaymentFirstPossibleMonth,
                AdvancedRepaymentFinePercentage =
                    loanProduct.AdvancedRepaymentFinePercentage,
                LoanProductId = loanProduct.Id,
                Requirements =
                    new LoanProductRequirementsViewModel
                    {
                        LoanProductRequirementsId = loanProduct.Requirements.Id,
                        MinWorkOnLastJobInMonths
                            =
                            loanProduct
                            .Requirements
                            .MinWorkOnLastJobInMonths,
                        MinSalary =
                            loanProduct
                            .Requirements
                            .MinSalary,
                        NeedEarningsRecord =
                            loanProduct
                            .Requirements
                            .NeedEarningsRecord,
                        NeedGuarantors =
                            loanProduct
                            .Requirements
                            .NeedGuarantors,
                        GuarantorsCount =
                            loanProduct
                            .Requirements
                            .GuarantorsCount,
                        Approvers =
                            loanProduct
                            .Requirements
                            .Approvers.Select(
                                identityRole =>
                                (Role)
                                Enum.Parse(
                                    typeof(Role),
                                    identityRole
                                    .Name))
                    }
            };
            return model;
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead)]
        [HttpPost]
        public ActionResult EditCredit(LoanProductViewModel model)
        {
            LoanProduct loanProduct = this._creditService.GetLoanProduct(model.LoanProductId);
            this.MapLoanProduct(model, loanProduct);
            this._creditService.Save(loanProduct);
            return this.Json(true);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead, 
            Role.Operator, Role.SecuritySpecialist)]
        public ActionResult GetApprovalRequests([DataSourceRequest] DataSourceRequest request)
        {
            Contract.Requires<ArgumentNullException>(request.IsNotNull());
            var requests = this.loanRequestsService.GetRequestsForApproval(this._userContext);
            var items = requests.Select(x => new ApprovalRequestViewModel
                                                 {
                                                     Amount = x.Amount,
                                                     FirstName = x.Client.FirstName,
                                                     LastName = x.Client.LastName,
                                                     Id = x.Id,
                                                     LoanProductName = x.LoanProduct.Name,
                                                     ClientId = x.Client.Id
                                                 });
            return this.Json(items.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentHead)]
        public ActionResult GetArchiveCredits([DataSourceRequest] DataSourceRequest request)
        {
            return this.GetCredits(request, LoanProductStatus.Archived);
        }

        public ActionResult GetAvailableCredits([DataSourceRequest] DataSourceRequest request)
        {
            return this.GetCredits(request, LoanProductStatus.Active);
        }

        [GangsterBankAuthorize(Role.Administrator)]
        public ActionResult GetAvailableCreditsForAdministrator([DataSourceRequest] DataSourceRequest request)
        {
            return this.GetCredits(request);
        }

        public ActionResult GetCreditCalculatorCreditData(int loanProductId)
        {
            var model = this.FillCreditCalculatorViewModel(loanProductId);
            return this.View("CreditCalulator/CreditData", model);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead)]
        public ActionResult GetDraftCredits([DataSourceRequest] DataSourceRequest request)
        {
            return this.GetCredits(request, LoanProductStatus.Draft);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentHead)]
        public ActionResult GetReadyForReviewCredits([DataSourceRequest] DataSourceRequest request)
        {
            return this.GetCredits(request, LoanProductStatus.ReadyForReview);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentSpecialist, Role.LendingDepartmentHead)]
        public ActionResult RemoveCredit(int creditId)
        {
            this._creditService.Remove(creditId);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        [GangsterBankAuthorize(Role.Administrator, Role.LendingDepartmentHead, Role.LendingDepartmentSpecialist)]
        public ActionResult SendToReview(int creditId)
        {
            return this.ChangeCreditStatus(creditId, LoanProductStatus.ReadyForReview);
        }

        [HttpGet]
        public ActionResult TakeCredit(string creditProductName, int creditProductId, int clientId)
        {
            var loanProduct = this._creditService.GetLoanProduct(creditProductId);
            var model = this.FillTakeCreditViewModel(creditProductName, creditProductId, clientId);
            model.MaxAmount = (int)loanProduct.MaxAmount;
            model.MinAmount = (int)loanProduct.MinAmount;
            model.MaxPeriod = loanProduct.MaxPeriodInMonth;
            model.MinPeriod = loanProduct.MinPeriodInMonth;
            return View("TakeCredit", model);
        }

        [HttpPost]
        public ActionResult TakeCredit(TakeCreditModel model)
        {
            Contract.Requires<ArgumentNullException>(model.IsNotNull());
            var error = "true";
            try
            {
                this.UpdateClientProfileTakenCredits(model);
            }
            catch (WorkflowException)
            {
                throw;
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            return this.Json(error);
        }

        #endregion

        #region Methods

        private ActionResult ChangeCreditStatus(int creditId, LoanProductStatus status)
        {
            var loanProduct = this._creditService.GetLoanProduct(creditId);
            loanProduct.Status = status;
            this._creditService.Save(loanProduct);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        private ConcreteCreditCalculatorModel FillConcreteCreditCalculatorModel(int loanProductId)
        {
            var loanProduct = this._creditService.GetLoanProduct(loanProductId);
            var model = new ConcreteCreditCalculatorModel();
            model.LoanProductId = loanProductId;
            model.LoanProductName = loanProduct.Name;
            return model;
        }

        private CreditCalculatorModel FillCreditCalculatorModel()
        {
            var model = new CreditCalculatorModel();
            var loanProducts = this._creditService.GetActiveLoanProducts();
            model.LoanProducts =
                loanProducts.Select(
                    x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(CultureInfo.InvariantCulture) });
            return model;
        }

        private CalculateCreditViewModel FillCreditCalculatorViewModel(int loanProductId)
        {
            var loanProduct = this._creditService.GetLoanProduct(loanProductId);
            var model = new CalculateCreditViewModel
                            {
                                MaxPeriod = loanProduct.MaxPeriodInMonth,
                                MinPeriod = loanProduct.MinPeriodInMonth,
                                MinAmount = (double)loanProduct.MinAmount,
                                MaxAmount = (double)loanProduct.MaxAmount,
                                MaxAmountD = loanProduct.MaxAmount,
                                MinAmountD = loanProduct.MinAmount,
                                LoanProductType = loanProduct.Type
                            };
            return model;
        }

        private TakeCreditViewModel FillTakeCreditViewModel(string creditProductName, int creditProductId, int clientId)
        {
            var model = new TakeCreditViewModel
                            {
                                CreditProductName = creditProductName, 
                                CreditProductId = creditProductId, 
                                ClientId = clientId
                            };
            return model;
        }

        private ActionResult GetCalculateCreditResult(CalculateCreditModel calculateCreditModel)
        {
            var calculatedData = this.creditManager.CalculateCredit(
                calculateCreditModel.LoanProductId, 
                calculateCreditModel.Amount, 
                calculateCreditModel.Monthes);

            // one more magic:) linque-magic:)
            var months = -calculatedData.Payments.Count() + 1;
            var model = new CalculationResultModel
                            {
                               
                                MonthlyPayments = calculatedData.Payments.Select(x => new MonthlyPaymentViewModel
                                        {
                                            Payment = x.ToGBString(),
                                            Month = DateTime.Today.AddMonths(months++).ToString("MMMMMMMMMM / yyyy", CultureInfo.InvariantCulture)
                                        }),
                                TotalPayment = calculatedData.TotalPayment.ToGBString()
                            };
            switch (calculateCreditModel.LoanProductType)
            {
                case LoanProductType.Anuitet:
                    return this.View("CreditCalulator/CalculationResult/AnuitetCalculationResult", model);
                case LoanProductType.Differential:
                    return this.View("CreditCalulator/CalculationResult/DifferencialCalculationResult", model);
                case LoanProductType.OneTimePayment:
                    return this.View("CreditCalulator/CalculationResult/OneTimePaymentCalculationResult", model);
                default:
                    throw new NotSupportedException();
            }
        }

        private ActionResult GetCredits(DataSourceRequest request, LoanProductStatus? filterStatus = null)
        {
            Contract.Requires<ArgumentNullException>(request.IsNotNull());
            var loanProducts = this._creditService.GetAvailableLoanProductsForShow();
            if (filterStatus != null)
            {
                loanProducts = loanProducts.Where(x => x.Status == filterStatus);
            }
            var result = loanProducts.Select(x => new AvailableCreditViewModel
                                                      {
                                                          MinAmount = x.MinAmount,
                                                          MaxAmount = x.MaxAmount,
                                                          MaxPeriodInMonth = x.MaxPeriodInMonth,
                                                          MinPeriodInMonth = x.MinPeriodInMonth,
                                                          Name = x.Name,
                                                          Percentage = x.Percentage,
                                                          Id = x.Id,
                                                          Description = x.Description
                                                      });
            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        private void UpdateClientProfileTakenCredits(TakeCreditModel model)
        {
            model.ClientId = this.ResolveClientId(model.ClientId);
            var client = this._clientProfileService.GetClient(model.ClientId);
            var loanProduct = this._creditService.GetLoanProduct(model.CreditProductId);
            this.loanRequestsService.CreateLoanRequest(client, loanProduct, model.Sum, model.PeriodInMonth);
        }

        #endregion
    }
}