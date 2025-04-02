using BookList.Data.Entities;
using BookList.Data.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data
{
    public class BookListDbContext : DbContext
    {
        public BookListDbContext(DbContextOptions<BookListDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            CreateSeedUsers(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        private static void CreateSeedUsers(ModelBuilder modelBuilder)
        {
            //create seed users with hashed passwords
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   Username = "admin",
                   PasswordHash = "$2a$11$WVCg6EN2qe/S6g.VA8bQ6OG9bcO2CXZxJnaAqnmsF4SNpzi3uQFPm", // Hash of "Admin@123"
                   Role = "Admin"
               },
               new User
               {
                   Id = 2,
                   Username = "user",
                   PasswordHash = "$2a$11$TaBHmICwPnLQUX/Qb/Msc.Di9okVQ9xdC15F7aGVah9M3ehIhZE4e", // Hash of "User@123"
                   Role = "User"
               });
        }
    }
}
