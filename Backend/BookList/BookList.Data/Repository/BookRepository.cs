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
        public BookRepository(BookListDbContext context):base(context) 
        {
            _context = context;
            
        }
        public async Task<Book?> GetBookByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        }
    }
}
