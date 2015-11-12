using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangsterBank.DataAccess.Extensions
{
    using System.Diagnostics.Contracts;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Entities.Base;

    public static class EnumerableExtensions
    {
        #region Public Methods and Operators

        public static IEnumerable<T> ActiveEntities<T>(this IEnumerable<T> set) where T : class, IEntity
        {
            Contract.Requires<ArgumentNullException>(set.IsNotNull());
            IEnumerable<T> activeEntities = from entity in set where !entity.IsDeleted select entity;

            return activeEntities;
        }

        #endregion
    }
}
