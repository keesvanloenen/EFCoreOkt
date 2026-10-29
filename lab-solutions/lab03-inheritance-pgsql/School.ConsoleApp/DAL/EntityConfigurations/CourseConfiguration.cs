using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL.EntityConfigurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Credits)
            .IsRequired();

        builder.UseTptMappingStrategy();
    }
}
