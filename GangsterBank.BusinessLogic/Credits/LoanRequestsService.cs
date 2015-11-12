namespace GangsterBank.BusinessLogic.Credits
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.Mail;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Accounts;
    using GangsterBank.Domain.Entities.Clients;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Clients.TakenLoan.Payment;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.EmailTemplates;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.Domain.Workflow;

    public class LoanRequestsService : ILoanRequestsService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        private readonly ILoanRequestPrerequisiteRule loanRequestPrerequisiteRule;

        private readonly ICreditManager creditManager;

        private readonly IMailService mailService;

        #endregion

        #region Constructors and Destructors

        public LoanRequestsService(
            IGangsterBankUnitOfWork gangsterBankUnitOfWork, 
            ILoanRequestPrerequisiteRule loanRequestPrerequisiteRule,
            ICreditManager creditManager,
            IMailService mailService)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanRequestPrerequisiteRule.IsNotNull());
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
            this.loanRequestPrerequisiteRule = loanRequestPrerequisiteRule;
            this.creditManager = creditManager;
            this.mailService = mailService;
        }

        #endregion

        #region Public Methods and Operators

        public void ApproveLoanRequest(LoanRequest loanRequest, IUserContext userContext)
        {
            Contract.Requires<ArgumentNullException>(userContext.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanRequest.IsNotNull());

            VerifyProfilePrerequisitesRuleCompleted(loanRequest);
            AddUserApprovers(loanRequest, userContext);
            this.CheckApprovedByAllApproversStatus(loanRequest);
            this.TakeLoan(loanRequest);
            this.gangsterBankUnitOfWork.LoanRequestsRepository.CreateOrUpdate(loanRequest);
        }

        public void ApproveLoanRequest(int loanRequestId, IUserContext userContext)
        {
            Contract.Requires<ArgumentNullException>(userContext.IsNotNull());
            Contract.Requires<ArgumentOutOfRangeException>(loanRequestId.IsPositive());

            LoanRequest loanRequest = this.gangsterBankUnitOfWork.LoanRequestsRepository.GetById(loanRequestId);
            this.ApproveLoanRequest(loanRequest, userContext);
        }

        public void CreateLoanRequest(Client client, LoanProduct loanProduct, decimal amount, int months)
        {
            Contract.Requires<ArgumentNullException>(client.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanProduct.IsNotNull());

            VerifyWorkFlow(client, loanProduct);
            LoanRequest loanRequest = LoanRequest.Create(client, loanProduct, amount, months);
            this.TryApproveAndTakeLoanOnCreation(loanRequest);

            this.gangsterBankUnitOfWork.LoanRequestsRepository.CreateOrUpdate(loanRequest);
        }

        public void DeclineLoanRequest(int loanRequestId, IUserContext userContext)
        {
            Contract.Requires<ArgumentNullException>(userContext.IsNotNull());
            Contract.Requires<ArgumentOutOfRangeException>(loanRequestId.IsPositive());

            LoanRequest loanRequest = this.gangsterBankUnitOfWork.LoanRequestsRepository.GetById(loanRequestId);
            this.DeclineLoanRequest(loanRequest, userContext);
        }

        public void DeclineLoanRequest(LoanRequest loanRequest, IUserContext userContext)
        {
            Contract.Requires<ArgumentNullException>(userContext.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanRequest.IsNotNull());

            VerifyProfilePrerequisitesRuleCompleted(loanRequest);
            SetDeclinedStatus(loanRequest, userContext);

            this.gangsterBankUnitOfWork.LoanRequestsRepository.CreateOrUpdate(loanRequest);
        }

        public IEnumerable<LoanRequest> GetRequestsForApproval(IUserContext userContext)
        {
            Contract.Requires<ArgumentNullException>(userContext.IsNotNull());
            IEnumerable<LoanRequest> loanRequests =
                this.gangsterBankUnitOfWork.LoanRequestsRepository.GetRequestsByStatusAndRemainingApprovers(
                    LoanRequestStatus.ProfilePrerequisitesAreVerified, 
                    userContext.IdentityRoleEntities);
            return loanRequests;
        }

        #endregion

        #region Methods

        private static void AddUserApprovers(LoanRequest loanRequest, IUserContext userContext)
        {
            IEnumerable<IdentityRoleEntity> userApprovers = GetUserApprovers(loanRequest, userContext).ToList();

            foreach (IdentityRoleEntity userRole in userApprovers)
            {
                loanRequest.ApprovedBy.Add(userRole);
            }
        }

        private static TakenLoan CreateTakenLoanForClient(LoanRequest loanRequest)
        {
            var takenLoan = new TakenLoan
                                {
                                    Amount = loanRequest.Amount, 
                                    MaturityInMonth = loanRequest.Months, 
                                    ProductLoan = loanRequest.LoanProduct, 
                                    TakeDate = DateTime.UtcNow,
                                    Payments = new Collection<LoanPayment>(),
                                    Status = TakenLoanStatus.Active
                                };
            loanRequest.Client.TakenLoans.Add(takenLoan);
            return takenLoan;
        }

        private static IEnumerable<IdentityRoleEntity> GetUserApprovers(
            LoanRequest loanRequest, 
            IUserContext userContext)
        {
            IEnumerable<IdentityRoleEntity> userApprovers =
                loanRequest.RemainingApprovers.Intersect(userContext.IdentityRoleEntities);
            return userApprovers;
        }

        private static void SetDeclinedStatus(LoanRequest loanRequest, IUserContext userContext)
        {
            IEnumerable<IdentityRoleEntity> userApprovers = GetUserApprovers(loanRequest, userContext);
            if (!userApprovers.Any())
            {
                throw new WorkflowException("User is not in remaining approvers list");
            }

            loanRequest.Status = LoanRequestStatus.Declined;
        }

        private static void VerifyClientIsConfirmed(Client client)
        {
            if (!client.IsConfirmed)
            {
                throw new WorkflowException("Client is not confirmed");
            }
        }

        private static void VerifyLoanProductIsNotActive(LoanProduct loanProduct)
        {
            if (loanProduct.Status != LoanProductStatus.Active)
            {
                throw new WorkflowException("Loan product is not active");
            }
        }

        private static void VerifyProfilePrerequisitesRuleCompleted(LoanRequest loanRequest)
        {
            if (loanRequest.Status != LoanRequestStatus.ProfilePrerequisitesAreVerified)
            {
                throw new WorkflowException("Loan Request should be verified by profile prerequisite rules first");
            }
        }

        private static void VerifyWorkFlow(Client client, LoanProduct loanProduct)
        {
            VerifyClientIsConfirmed(client);
            VerifyLoanProductIsNotActive(loanProduct);
        }

        private void CheckApprovedByAllApproversStatus(LoanRequest loanRequest)
        {
            if (!loanRequest.RemainingApprovers.Any())
            {
                loanRequest.Status = LoanRequestStatus.ApprovedByAllApprovers;
            }
        }

        private string CheckLoanRequestPrerequisiteRules(LoanRequest loanRequest)
        {
            var result = this.loanRequestPrerequisiteRule.IsValid(loanRequest);
            loanRequest.Status = result == string.Empty
                                     ? LoanRequestStatus.ProfilePrerequisitesAreVerified
                                     : LoanRequestStatus.Declined;
            return result;
        }

        private void TakeLoan(LoanRequest loanRequest)
        {
            if (loanRequest.Status != LoanRequestStatus.ApprovedByAllApprovers)
            {
                return;
            }

            loanRequest.Status = LoanRequestStatus.Approved;
            this.mailService.SendMessage(EmailTemplateType.ApproveCreditRequest, loanRequest.Client, loanRequest);
            TakenLoan takenLoan = CreateTakenLoanForClient(loanRequest);
            Account account = loanRequest.Client.PrimaryAccount;
            account.Amount += takenLoan.Amount;
            this.AddLoanPayments(takenLoan);

            this.gangsterBankUnitOfWork.ClientsRepository.CreateOrUpdate(loanRequest.Client);
        }

        private void AddLoanPayments(TakenLoan takenLoan)
        {
            var payments = creditManager.GetMonthlyPayments(takenLoan);
            var date = DateTime.Now.Date.AddMonths(1);
            foreach (var payment in payments)
            {
                if (payment != 0)
                {
                    takenLoan.Payments.Add(this.CreatePayment(payment, date));
                }
                date = date.AddMonths(1);
            }
        }

        private LoanPayment CreatePayment(decimal amount, DateTime date)
        {
            return new LoanPayment
                       {
                           Amount = amount,
                           Date = date,
                           Status = LoanPaymentStatus.Active
                       };
        }

        private void TryApproveAndTakeLoanOnCreation(LoanRequest loanRequest)
        {
            var result = this.CheckLoanRequestPrerequisiteRules(loanRequest);
            if (loanRequest.Status == LoanRequestStatus.Declined)
            {
               this.gangsterBankUnitOfWork.LoanRequestsRepository.CreateOrUpdate(loanRequest);
               throw new Exception(result);
            }
            this.CheckApprovedByAllApproversStatus(loanRequest);
            this.TakeLoan(loanRequest);
        }

        #endregion
    }
}