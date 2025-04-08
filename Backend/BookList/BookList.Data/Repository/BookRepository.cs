using BookList.Data.Entities;
using BookList.Data.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly BookListDbContext _context;
        public BookRepository(BookListDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task<Book?> GetBookByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        }
        public async Task<IEnumerable<Book>> GetAllWithAuthorsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(IEnumerable<Book>, int)> GetBooksPaginatedAsync(int page, int size, string searchQuery, CancellationToken cancellation)
        {
            var skip = (page - 1) * size;

            var query = _context.Books
                .Include(b => b.Author) // Required for AuthorName in DTO
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(b => b.Title.Contains(searchQuery) || b.ISBN!.Contains(searchQuery));

            }

            var totalCount = await query.CountAsync(cancellation);

            var books = await query
                .OrderBy(b => b.Title)
                .Skip(skip)
                .Take(size)
                .ToListAsync(cancellation);

            return (books, totalCount);
        }
    }
}
