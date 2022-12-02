using Application.Repositories;
using Core.Interfaces;
using Infrastructure;

namespace Application
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DbDataContext _context;
        private readonly IGenericRepositroy<T> _entity;

        public UnitOfWork(DbDataContext dbDataContext)
        {
            _context = dbDataContext;
        }
        public IGenericRepositroy<T> Entity
        {
            get
            {
                return _entity ?? new GenericRepository<T>(_context);
            }
        }

        public async Task<bool> CompeleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync()
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       

    }
}
