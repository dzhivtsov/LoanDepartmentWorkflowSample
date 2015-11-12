namespace GangsterBank.DataAccess.Repositories
{
    #region

    using System.Collections.Generic;
    using System.Linq;

    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.Domain.Entities.Credits;

    #endregion

    public class LoanProductsRepository : BaseRepository<LoanProduct>, ILoanProductsRepository
    {
        #region Constructors and Destructors

        public LoanProductsRepository(GangsterBankContext context)
            : base(context)
        {
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<LoanProduct> GetAllLoanProducts()
        {
            return this.DbSet;
        }

        public IEnumerable<LoanProduct> GetLoanProductsByStatus(LoanProductStatus status)
        {
            IQueryable<LoanProduct> loanProducts = from loanProduct in this.DbSet
                                                   where loanProduct.Status == status
                                                   select loanProduct;
            return loanProducts;
        }

        #endregion
    }
}