using Microsoft.EntityFrameworkCore;
using School.Library.DAL.EntityConfigurations;
using School.Library.Models;

namespace School.Library.DAL;

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
    public DbSet<Enrollment> Enrollments { get; set; }

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
	{
	}

    public SchoolContext()
    {
    }

    // 👇 Override OnConfiguring to support migrations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 👇 The program wasn't started by the console app apparently
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Port=5432;Database=SchoolV3;Username=postgres;Password=Pa$$w0rd");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("school");

        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());

        modelBuilder.ApplyConfiguration(new DepartmentAdministratorConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorConfiguration());
        modelBuilder.ApplyConfiguration(new OnlineCourseConfiguration());
        modelBuilder.ApplyConfiguration(new OnsiteCourseConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
    }
}
