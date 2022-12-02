using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepositroy<T> where T : class
    {
       Task<IEnumerable<T>>GetAll();
        Task<T> GetTById(object Id);
        Task  Insert(T entity);
        Task Update(T entity);
        Task<bool> Delete(object Id);
    }
}
