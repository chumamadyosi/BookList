using BookList.Data.Entities;
using BookList.Data.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetBookByTitleAsync(string title);
        Task<IEnumerable<Book>> GetAllWithAuthorsAsync();
        Task<(IEnumerable<Book>, int)> GetBooksPaginatedAsync(int page, int size, string searchQuery, CancellationToken cancellation);
    }
}
