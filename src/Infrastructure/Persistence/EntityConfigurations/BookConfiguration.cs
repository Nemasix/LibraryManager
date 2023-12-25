using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));
            builder.HasKey(book => book.Id);
            builder.Property(book => book.Id).ValueGeneratedOnAdd();
            builder.Property(book => book.Title).IsRequired().HasMaxLength(200);
            builder.Property(book => book.Author).IsRequired().HasMaxLength(200);
        }
    }
}