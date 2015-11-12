namespace GangsterBank.DataAccess.Contracts.Repositories
{
    using System.Collections.Generic;

    using GangsterBank.Domain.Entities.Base;

    public interface IRepository<T>
        where T : class, IEntity
    {
        #region Public Methods and Operators

        void CreateOrUpdate(T entity);

        T GetById(int id);

        void Remove(T entity);

        IEnumerable<T> GetAll();

        #endregion
    }
}