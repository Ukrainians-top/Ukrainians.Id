using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UkrainiansId.Domain.Models;
namespace UkrainiansId.Domain.Interfaces
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : BaseModel
    {
        Task<IQueryable<T>> ReadFromSqlAsync(string sql, params object[] parameters);
        Task ExecuteSqlAsync(string sql);
    }

    public interface IReadRepository<TEntity> where TEntity : BaseModel
    {
        #region GetAll

        Task<IList<TEntity>> GetAllAsync(bool isTracking = false);

        Task<IList<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            bool isTracking = false);

        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = false,
            bool ignoreQueryFilters = false);

        Task<IList<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        #endregion

        #region GetFirst

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           bool disableTracking = true,
           bool ignoreQueryFilters = false);

        Task<TResult> GetFirstAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        #endregion

        #region GetPagedList

        Task<List<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false);

        Task<List<TResult>> GetPagedListAsync<TResult>(
           Expression<Func<TEntity, TResult>> selector,
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           int pageIndex = 0,
           int pageSize = 20,
           bool disableTracking = true,
           CancellationToken cancellationToken = default(CancellationToken),
           bool ignoreQueryFilters = false) where TResult : class;

        #endregion

        #region Counter

        Task<int> CountAsync(Expression<Func<TEntity, bool>> match = null);
        Task<long> CountLongAsync(Expression<Func<TEntity, bool>> match = null);

        #endregion

        #region IsExist

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> match);
        Task<bool> IsExistAsync();

        #endregion

        #region Aggregations

        Task<T> MaxAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default);

        Task<T> MinAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default);

        Task<decimal> AverageAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default);

        Task<decimal> SumAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default);

        #endregion
    }

    public interface IWriteRepository<TEntity> where TEntity : BaseModel
    {

        #region Create

        Task<TEntity> CreateAsync(TEntity entity);
        Task<List<TEntity>> CreateListAsync(List<TEntity> entities);

        #endregion

        #region Update

        TEntity Update(TEntity entity);
        List<TEntity> UpdateList(List<TEntity> entities);

        #endregion

        #region Delete

        TEntity Delete(TEntity entity);
        List<TEntity> DeleteList(List<TEntity> entities);

        #endregion

        #region SaveChanges

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync();

        #endregion
    }
}