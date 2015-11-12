namespace GangsterBank.DataAccess.Extensions
{
    #region

    using System;
    using System.Data.Entity;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Base;

    #endregion

    public static class DbSetExtensions
    {
        #region Public Methods and Operators

        public static IQueryable<T> ActiveEntities<T>(this DbSet<T> set) where T : class, IEntity
        {
            Contract.Requires<ArgumentNullException>(set.IsNotNull());
            IQueryable<T> activeEntities = from entity in set where !entity.IsDeleted select entity;
            return activeEntities;
        }

        #endregion
    }
}