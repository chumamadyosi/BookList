using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username).IsRequired(true).HasMaxLength(50);

            builder.Property(x => x.PasswordHash).IsRequired(true).HasMaxLength(255);

            builder.Property(x => x.Role).IsRequired(true).HasMaxLength(20);
        }
    }
}
