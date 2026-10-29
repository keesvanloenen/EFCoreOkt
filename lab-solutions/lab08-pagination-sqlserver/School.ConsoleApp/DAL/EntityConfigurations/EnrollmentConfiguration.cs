using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL.EntityConfigurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => new { e.StudentId, e.CourseId });

        builder.Property(e => e.Grade)
            .IsRequired(false)
            .HasColumnType("decimal(3,2)");
    }
}