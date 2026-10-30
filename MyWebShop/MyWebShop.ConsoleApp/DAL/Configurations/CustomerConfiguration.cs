using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebShop.ConsoleApp.Model;
using System.Reflection.Emit;

namespace MyWebShop.ConsoleApp.DAL.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(c => c.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired(false);

        builder
            .Property(c => c.CreditLimit)
            .HasColumnType("decimal(8,2)");

        builder
            .Property(c => c.CreatedAt)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder
            .Property(c => c.CreatedAt)
            .HasColumnName("CustomerNumber");

        builder
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);

        builder.Property(c => c.Rowversion)
            .IsRowVersion();
    }
}
