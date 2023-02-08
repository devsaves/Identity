using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApi.Respository.interfaces
{
    public interface IRepository<T> where T : class
    {
        public void Add(T entity);
        public void AddList(T[] entity);
        public void Update(T entity);
        public void Delete(T entity);
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
        public Task<bool> save();

    }
}