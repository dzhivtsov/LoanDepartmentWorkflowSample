namespace GangsterBank.DataAccess.Repositories
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Core.Extensions;
    using GangsterBank.DataAccess.Contracts.Repositories;
    using GangsterBank.DataAccess.Extensions;
    using GangsterBank.Domain.Entities.Base;
    using GangsterBank.Domain.Exceptions;

    #endregion

    public class BaseRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        #region Constructors and Destructors

        public BaseRepository(GangsterBankContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            this.Context = context;
        }

        #endregion

        #region Properties

        protected IQueryable<T> ActiveEntities
        {
            get
            {
                return this.DbSet.ActiveEntities();
            }
        }

        protected GangsterBankContext Context { get; private set; }

        protected DbSet<T> DbSet
        {
            get
            {
                return this.Context.Set<T>();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void CreateOrUpdate(T entity)
        {
            Contract.Requires<ArgumentNullException>(entity != null);
            this.Context.Entry(entity).State = entity.Id == default(int) ? EntityState.Added : EntityState.Modified;
            this.Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet;
        }

        public T GetById(int id)
        {
            Contract.Requires<ArgumentException>(id > 0);
            T entity = this.DbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public void Remove(T entity)
        {
            Contract.Requires<ArgumentNullException>(entity != null);
            entity.IsDeleted = true;
            this.CreateOrUpdate(entity);
        }

        #endregion

        #region Methods

        protected void ThrowNotFoundException<TEntity>(TEntity value) where TEntity : IEntity
        {
            if (value.IsNotNull())
            {
                return;
            }

            throw new NotFoundException();
        }

        protected void ThrowNotFoundException<TEntity>(IQueryable<TEntity> value) where TEntity : IEntity
        {
            this.ThrowNotFoundException(value.AsEnumerable());
        }

        protected void ThrowNotFoundException<TEntity>(IEnumerable<TEntity> value) where TEntity : IEntity
        {
            if (value.IsNotNull() || value.Any())
            {
                return;
            }

            throw new NotFoundException();
        }

        #endregion
    }
}