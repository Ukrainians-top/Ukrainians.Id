using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UkrainiansId.Domain.Models;
namespace UkrainiansId.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> CreateListAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateListAsync(List<T> entities);
        Task<T> DeleteAsync(T entity);
        Task<List<T>> DeleteListAsync(List<T> entities);
        Task<List<T>> GetAllAsync(bool all = false, int skip = 0, int take = 20, bool isTracking = false);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> match, bool isTracking = false);
        Task<int> CountAsync(Expression<Func<T, bool>> match = null);
        Task<long> CountLongAsync(Expression<Func<T, bool>> match = null);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> match);
        Task<int> SaveChangesAysnc();
        Task<IQueryable<T>> FromSqlAsync(string sql, params object[] parameters);
    }
}