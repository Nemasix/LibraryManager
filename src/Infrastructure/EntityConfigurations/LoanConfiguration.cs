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
    internal class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable(nameof(Loan));
            builder.HasKey(loan => loan.Id);
            builder.Property(loan => loan.Id).ValueGeneratedOnAdd();
            builder.Property(loan => loan.LoanDate).IsRequired();
            builder.Property(loan => loan.ReturnDate).IsRequired();
            builder.Property(loan => loan.BookId).IsRequired();
            builder.Property(loan => loan.LoanerId).IsRequired();
        }
    }
}
