using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.Couchbase.Abstract
{
    public interface ICouchbaseRepository<T,TId> where T : IEntity<TId>
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<int> Count(Expression<Func<T, bool>> filter = null);
    }
}
