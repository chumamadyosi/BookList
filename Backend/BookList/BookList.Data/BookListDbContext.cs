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
            CreateSeedAuthorsWithBooks(modelBuilder);
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
        private static void CreateSeedAuthorsWithBooks(ModelBuilder modelBuilder)
        {
            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 1,
                    Name = "J.K. Rowling"
                },
                new Author
                {
                    AuthorId = 2,
                    Name = "George R. R. Martin"
                }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    AuthorId = 1, // Link to J.K. Rowling
                    PublishedYear = 1997,
                    ISBN = "9780747532743",
                    Description = "The first book in the Harry Potter series."
                },
                new Book
                {
                    BookId = 2,
                    Title = "A Game of Thrones",
                    AuthorId = 2, // Link to George R. R. Martin
                    PublishedYear = 1996,
                    ISBN = "9780553103540",
                    Description = "The first book in the A Song of Ice and Fire series."
                }
            );
        }
    }
}
