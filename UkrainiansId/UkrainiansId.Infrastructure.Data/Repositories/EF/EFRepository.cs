using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UkrainiansId.Domain.Interfaces;
using UkrainiansId.Domain.Models;
using UkrainiansId.Infrastructure.Data.EntityFramework.Context;

namespace UkrainiansId.Infrastructure.Data.Repositories.EF
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        protected readonly UkrainiansIdContext _db;
        protected readonly DbSet<TEntity> dbSet;

        public EFRepository(UkrainiansIdContext db)
        {
            _db = db;
            dbSet = _db.Set<TEntity>();
        }

        public async Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
         =>
            predicate is null
                ? await dbSet.AverageAsync(selector, cancellationToken)
                : await dbSet.Where(predicate).AverageAsync(selector, cancellationToken);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> match = null) => match == null ? await dbSet.CountAsync() : await dbSet.CountAsync(match);

        public async Task<long> CountLongAsync(Expression<Func<TEntity, bool>> match = null) => match == null ? await dbSet.LongCountAsync() : await dbSet.LongCountAsync(match);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await dbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task<List<TEntity>> CreateListAsync(List<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return entities;
        }

        public TEntity Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            return entity;
        }

        public List<TEntity> DeleteList(List<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return entities;
        }

        public Task ExecuteSqlAsync(string sql)
        {
            dbSet.FromSqlRaw(sql);
            return Task.CompletedTask;
        }

        public async Task<IList<TEntity>> GetAllAsync(bool isTracking = false) => isTracking ? await dbSet.ToListAsync() : await dbSet.AsNoTracking().ToListAsync();

        public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, bool isTracking = false)
        => !isTracking
                ? await dbSet.AsNoTracking().Select(selector).ToListAsync()
                : await dbSet.Select(selector).ToListAsync();

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = false, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
        =>
            disableTracking
                ? await dbSet.AsNoTracking().Select(selector).ToListAsync()
                : await dbSet.Select(selector).ToListAsync();

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<TResult> GetFirstAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.Skip(pageIndex).Take(pageSize).ToListAsync();
        }

        public async Task<List<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false) where TResult : class
        {
            IQueryable<TEntity> query = dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.Select(selector).Skip(pageIndex).Take(pageSize).ToListAsync();
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> match)
        {
            if (match == null)
                return await dbSet.AnyAsync();
            return await dbSet.AnyAsync(match);
        }

        public async Task<bool> IsExistAsync()
        {
            return await IsExistAsync(null);
        }

        public async Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
        =>
            predicate is null
                ? await dbSet.MaxAsync(selector, cancellationToken)
                : await dbSet.Where(predicate).MaxAsync(selector, cancellationToken);

        public async Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
        =>
            predicate is null
                ? await dbSet.MinAsync(selector, cancellationToken)
                : await dbSet.Where(predicate).MinAsync(selector, cancellationToken);

        public async Task<IQueryable<TEntity>> ReadFromSqlAsync(string sql, params object[] parameters) => (await dbSet.FromSqlRaw(sql, parameters).ToListAsync()).AsQueryable();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }

        public Task SaveChangesAsync()
        {
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
        =>
            predicate is null
                ? await dbSet.SumAsync(selector, cancellationToken)
                : await dbSet.Where(predicate).SumAsync(selector, cancellationToken);

        public TEntity Update(TEntity entity)
        {
            dbSet.Update(entity);
            return entity;
        }

        public List<TEntity> UpdateList(List<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return entities;
        }
    }
}