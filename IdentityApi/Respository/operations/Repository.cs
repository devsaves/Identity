using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityApi.Respository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Respository.operations
{

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IdDbContext _CONTEXT;
        public Repository(IdDbContext CONTEXT)
        {
            _CONTEXT = CONTEXT;
        }
        public void Add(T entity)
        {
            _CONTEXT.Set<T>().AddAsync(entity);
        }
        public void AddList(T[] entity)
        {
            _CONTEXT.Set<T[]>().AddRange(entity);
        }

        public void Delete(T entity)
        {
            _CONTEXT.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = await _CONTEXT.Set<T>().AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _CONTEXT.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);

            return result;
        }

        public async Task<bool> save()
        {
            return await _CONTEXT.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _CONTEXT.Entry(entity).CurrentValues.SetValues(entity);
            _CONTEXT.Set<T>().Update(entity);
        }
    }
}