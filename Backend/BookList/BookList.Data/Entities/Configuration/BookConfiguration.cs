using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Entities.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.PublishedYear)
                .IsRequired(false);

            builder.Property(b => b.ISBN)
                .HasMaxLength(20);

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index on Title for performance (querying books by Title) 
            // Index on ISBN for performance (querying books by ISBN)
            // Composite Index on AuthorId and PublishedYear for fast queries involving both columns
            // Unique Index on ISBN to enforce uniqueness
            builder.HasIndex(b => b.Title)
                .HasDatabaseName("IX_Books_Title");
           
            builder.HasIndex(b => b.ISBN)
                .HasDatabaseName("IX_Books_ISBN");
     
            builder.HasIndex(b => new { b.AuthorId, b.PublishedYear })
                .HasDatabaseName("IX_Books_AuthorId_PublishedYear");
    
            builder.HasIndex(b => b.ISBN)
                .IsUnique()
                .HasDatabaseName("IX_Books_ISBN_Unique");

          
        }
    }
}
