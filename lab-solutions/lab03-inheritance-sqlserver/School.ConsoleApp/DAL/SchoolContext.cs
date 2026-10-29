using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL.EntityConfigurations;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL;

public class SchoolContext: DbContext
{
	public DbSet<Person> People { get; set; }
	public DbSet<Course> Courses { get; set; }
    public DbSet<OnlineCourse> OnlineCourses { get; set; }
    public DbSet<OnsiteCourse> OnsiteCourses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<DepartmentAdministrator> DepartmentAdministrators { get; set; }

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
    }
}
