using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL.EntityConfigurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(d => d.Budget)
            .HasColumnType("Decimal(8,2)")
            .IsRequired();

        builder.Property(d => d.StartDate)
            .IsRequired();
    }
}
