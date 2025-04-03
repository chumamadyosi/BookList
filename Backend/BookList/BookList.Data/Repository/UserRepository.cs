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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BookListDbContext _context;
        public UserRepository(BookListDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
