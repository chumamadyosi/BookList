using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data
{
    public class BookListDbContextFactory : IDesignTimeDbContextFactory<BookListDbContext>
    {
        public BookListDbContextFactory() { }

        public BookListDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BookListDbContext>();
            var connectionString = configuration.GetConnectionString("BookListConnection");
            builder.UseSqlServer(connectionString);
            return new BookListDbContext(builder.Options);
        }
    }
}
