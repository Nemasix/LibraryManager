using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(user => user.LastName).IsRequired().HasMaxLength(200);
            builder.Property(user => user.UserName).IsRequired().HasMaxLength(200);
            builder.Property(user => user.Email).IsRequired().HasMaxLength(200);
            builder.Property(user => user.Password).IsRequired().HasMaxLength(200);
            builder.HasMany(user => user.Books)
                .WithOne()
                .HasForeignKey(book => book.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.Loans)
                .WithOne()
                .HasForeignKey(loan => loan.LoanerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
