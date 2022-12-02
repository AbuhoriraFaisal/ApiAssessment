using Core.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class GenericRepository<T> : IGenericRepositroy<T> where T : class
    {
        private readonly DbDataContext _dbContext;
        private DbSet<T> _table = null;

        public GenericRepository(DbDataContext dbDataContext)
        {
            _dbContext = dbDataContext;
            _table = dbDataContext.Set<T>();
        }


        async Task<bool> IGenericRepositroy<T>.Delete(object Id)
        {
            try
            {
                T? existing =  _table.Find(Id);
                if (existing != null)
                {
                    _table.Remove(existing);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        async Task<IEnumerable<T>> IGenericRepositroy<T>.GetAll()
        {
            try
            {
                return await _table.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        async Task<T> IGenericRepositroy<T>.GetTById(object Id)
        {
            T? existing = await _table.FindAsync(Id);
            if (existing == null)
                return null;
            _table.Entry(existing).State = EntityState.Detached;
            return existing;
        }

        async Task IGenericRepositroy<T>.Insert(T entity)
        {
            await _table.AddAsync(entity);
        }

        async Task IGenericRepositroy<T>.Update(T entity)
        {
            _table.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
