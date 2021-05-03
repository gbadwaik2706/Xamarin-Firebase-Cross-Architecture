using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FCANoti.Web.Repository.Helper
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all records as an IQueryable to support arbitrary filtering, sorting, paging, inlcudes, etc.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();
        IQueryable<TEntity> GetQueryWithChildren(string children);
        /// <summary>
        /// Get a tracked IQueryable to support getting records for updating using arbitrary filtering.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetTrackedQuery();

        /// <summary>
        /// Get a record by ID.
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>The record, or null if not found.</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Create a new entity object, using a proxy if the entity has navigation properties.
        /// </summary>
        /// <returns></returns>
        /// <remarks>The instance is not added or attached; call Add to actually add it.</remarks>
        //TEntity Create();

        ///// <summary>
        ///// Add a new record.
        ///// </summary>
        ///// <param name="entity"></param>
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Update a record.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete a record by its ID.
        /// </summary>
        /// <param name="id">Primary key</param>
        void DeleteById(object id);

        /// <summary>
        /// Undo pending changes to an entity.
        /// </summary>
        /// <param name="entity"></param>
        void UndoChanges(TEntity entity);

        bool SaveChanges();

        /// <summary>
        /// Get all records as an IQueryable to support arbitrary filtering, sorting, paging, inlcudes, etc.
        /// </summary>
        /// <param name="expresssion"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expresssion);
    }
}
