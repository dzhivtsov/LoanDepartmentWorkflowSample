namespace GangsterBank.BusinessLogic.Credits
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.UnitsOfWork;
    using GangsterBank.Domain.Entities.Clients.TakenLoan;
    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Workflow;

    #endregion

    public class CreditService : ICreditService
    {
        #region Fields

        private readonly IGangsterBankUnitOfWork gangsterBankUnitOfWork;

        private readonly LoanProductCreationWorkflowConfiguration loanProductCreationWorkflowConfiguration;

        private readonly IUserContext userContext;

        #endregion

        #region Constructors and Destructors

        public CreditService(
            IGangsterBankUnitOfWork gangsterBankUnitOfWork, 
            LoanProductCreationWorkflowConfiguration loanProductCreationWorkflowConfiguration, 
            IUserContext userContext)
        {
            Contract.Requires<ArgumentNullException>(gangsterBankUnitOfWork.IsNotNull());
            Contract.Requires<ArgumentNullException>(loanProductCreationWorkflowConfiguration.IsNotNull());
            Contract.Requires<ArgumentNullException>(userContext.IsNotNull());
            this.loanProductCreationWorkflowConfiguration = loanProductCreationWorkflowConfiguration;
            this.gangsterBankUnitOfWork = gangsterBankUnitOfWork;
            this.userContext = userContext;
        }

        #endregion

        #region Public Methods and Operators

        public void ChangeLoanProductStatus(LoanProduct loanProduct, LoanProductStatus newStatus)
        {
            Contract.Requires<ArgumentNullException>(loanProduct.IsNotNull());
            this.VerifyLoanProductCreationWorkflow(loanProduct, newStatus);
            loanProduct.Status = newStatus;
            this.gangsterBankUnitOfWork.LoanProductsRepository.CreateOrUpdate(loanProduct);
        }

        public void ChangeLoanProductStatus(int loanProductId, LoanProductStatus status)
        {
            Contract.Requires<ArgumentOutOfRangeException>(loanProductId.IsPositive());
            LoanProduct loanProduct = this.gangsterBankUnitOfWork.LoanProductsRepository.GetById(loanProductId);
            this.ChangeLoanProductStatus(loanProduct, status);
        }

        public bool CreateLoanProduct(LoanProduct loanProduct)
        {
            loanProduct.Status = LoanProductStatus.Draft;
            this.gangsterBankUnitOfWork.LoanProductsRepository.CreateOrUpdate(loanProduct);
            //this.gangsterBankUnitOfWork.LoanProductsRequirmentsRepository.CreateOrUpdate(loanProduct.Requirements);
            return true;
        }

        public IEnumerable<LoanProduct> GetActiveLoanProducts()
        {
            IEnumerable<LoanProduct> loanProducts = this.GetLoanProductsByStatus(LoanProductStatus.Active);
            return loanProducts;
        }

        public IEnumerable<LoanProduct> GetArchivedLoanProducts()
        {
            IEnumerable<LoanProduct> loanProducts = this.GetLoanProductsByStatus(LoanProductStatus.Archived);
            return loanProducts;
        }

        public IEnumerable<LoanProduct> GetAvailableLoanProducts()
        {
            return this.gangsterBankUnitOfWork.LoanProductsRepository.GetAllLoanProducts();
        }

        /// <summary>
        /// This method removes circular to prevent error in javascript serializer
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LoanProduct> GetAvailableLoanProductsForShow()
        {
            var loanProducts = this.gangsterBankUnitOfWork.LoanProductsRepository.GetAllLoanProducts();
            var result = loanProducts.ToList();
            result.ForEach(x => x.Requirements.Approvers.ForEach(y => y.LoanProductRequirements.Clear()));
            return result;
        }

        public IEnumerable<LoanProduct> GetDraftLoanProducts()
        {
            IEnumerable<LoanProduct> loanProducts = this.GetLoanProductsByStatus(LoanProductStatus.Draft);
            return loanProducts;
        }

        public LoanProduct GetLoanProduct(int id)
        {
            return this.gangsterBankUnitOfWork.LoanProductsRepository.GetById(id);
        }

        public IEnumerable<LoanProduct> GetReadyForReviewLoanProducts()
        {
            IEnumerable<LoanProduct> loanProducts = this.GetLoanProductsByStatus(LoanProductStatus.ReadyForReview);
            return loanProducts;
        }

        public bool Remove(int loanProductId)
        {
            LoanProduct entity = this.gangsterBankUnitOfWork.LoanProductsRepository.GetById(loanProductId);
            this.gangsterBankUnitOfWork.LoanProductsRepository.Remove(entity);
            return true;
        }

        public IEnumerable<TakenLoan> GetAllTakenLoans()
        {
            return this.gangsterBankUnitOfWork.TakenLoansRepository.GetAll();
        }

        public void Save(LoanProduct loanProduct)
        {
            Contract.Requires<ArgumentNullException>(loanProduct.IsNotNull());
            this.gangsterBankUnitOfWork.LoanProductsRepository.CreateOrUpdate(loanProduct);
        }

        public IEnumerable<LoanProduct> GetAlLoanProducts()
        {
            return this.gangsterBankUnitOfWork.LoanProductsRepository.GetAll();
        }

        #endregion

        #region Methods

        private IEnumerable<LoanProduct> GetLoanProductsByStatus(LoanProductStatus status)
        {
            IEnumerable<LoanProduct> loanProducts =
                this.gangsterBankUnitOfWork.LoanProductsRepository.GetLoanProductsByStatus(status);
            return loanProducts;
        }

        private bool LoanProductHasPrerequisiteStatus(LoanProduct loanProduct, LoanProductStatus newStatus)
        {
            return this.loanProductCreationWorkflowConfiguration.CanHaveStatus(loanProduct, newStatus);
        }

        private bool UserIsAllowedToChangeStatus(LoanProductStatus newStatus)
        {
            return this.loanProductCreationWorkflowConfiguration.IsAllowedToChangeStatus(
                newStatus, 
                this.userContext.Roles.ToArray());
        }

        private void VerifyLoanProductCreationWorkflow(LoanProduct loanProduct, LoanProductStatus newStatus)
        {
            if (!this.LoanProductHasPrerequisiteStatus(loanProduct, newStatus))
            {
                throw new WorkflowException("Loan product doesn't have prerequisite status");
            }

            if (!this.UserIsAllowedToChangeStatus(newStatus))
            {
                throw new WorkflowException("User is not allowed to change the status");
            }
        }

        #endregion
    }
}