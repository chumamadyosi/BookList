using BookList.Data.Entities;
using BookList.Data.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Repository
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly BookListDbContext _context;
        public AuthorRepository(BookListDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
