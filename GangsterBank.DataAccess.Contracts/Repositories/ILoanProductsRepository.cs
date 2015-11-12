namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Credits;

    public interface ILoanProductsRepository : IRepository<LoanProduct>
    {
        IEnumerable<LoanProduct> GetAllLoanProducts();

        IEnumerable<LoanProduct> GetLoanProductsByStatus(LoanProductStatus status);
    }
}
