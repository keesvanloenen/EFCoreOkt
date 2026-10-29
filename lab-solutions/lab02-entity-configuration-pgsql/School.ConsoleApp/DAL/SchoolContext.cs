using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL.EntityConfigurations;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL;

public class SchoolContext: DbContext
{
	public DbSet<Person> People { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Department> Departments { get; set; }

	public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("school");

        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
    }
}
