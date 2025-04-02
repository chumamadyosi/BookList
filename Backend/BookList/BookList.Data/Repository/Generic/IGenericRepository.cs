using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
