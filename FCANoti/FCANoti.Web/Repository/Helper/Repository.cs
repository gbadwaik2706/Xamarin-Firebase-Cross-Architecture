using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FCANoti.Web.Repository.Helper
{
    public class Repository<TEntity> : IRepository<TEntity>
             where TEntity : class
    {
        protected FCANotiDbContext DbContext { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }

        public Repository(FCANotiDbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <summary>
        /// Get all records as an IQueryable to support arbitrary filtering and includes.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The entities are not tracked in the cache for better performance so should be considered read-only.
        /// Use GetById or GetTrackedQuery to get entities for use in updates.
        /// </remarks>
        public virtual IQueryable<TEntity> GetQuery()
        {
            return DbSet.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// Get a tracked IQueryable to support getting records for updating using arbitrary filtering.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetTrackedQuery()
        {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Get a record by ID.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>The record, or null if not found.</returns>
        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
            //return entity;
        }

        /// <summary>
        /// Add a new range of records.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
            //int resCount = EntitiesContext.SaveChanges();
            //return resCount > 0;
        }

        public virtual async void AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await DbSet.AddRangeAsync(entity);
            //int resCount = EntitiesContext.SaveChanges();
            //return resCount > 0;
        }

        /// <summary>
        /// Update a record.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            DbSet.AsNoTracking();
            DbContext.Entry(entity).State = EntityState.Modified;
            //EntitiesContext.SaveChanges();
            //EntitiesContext.Entry(entity).State = EntityState.Detached;
            //return true;
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.AsNoTracking();
            DbContext.Entry(entity).State = EntityState.Deleted;
            //DbSet.Remove(entity);
            //DbContext.SaveChanges();
            //DbContext.Entry(entity).State = EntityState.Detached;
            //return true;
        }

        /// <summary>
        /// Delete a record by its ID.
        /// </summary>
        /// <param name="id">Primary key</param>
        public virtual void DeleteById(object id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
            //return entity;
        }

        /// <summary>
        /// Undo pending changes to an entity.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void UndoChanges(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            var state = entry.State;
            switch (state)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;

                case EntityState.Deleted:
                case EntityState.Modified:
                    // Setting the state to Unchanged restores the original values.
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Unchanged:
                case EntityState.Detached:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    // Calling SetValues changes the state to Modified, so restore the original state.
                    entry.State = state;
                    break;
            }
        }

        public IQueryable<TEntity> GetQueryWithChildren(string children)
        {
            return DbSet.Include(children).AsNoTracking().AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expresssion)
        {
            return DbSet.Where(expresssion);
        }

        public bool SaveChanges()
        {
            int resCount = DbContext.SaveChanges();
            return resCount > 0;
        }
    }
}
