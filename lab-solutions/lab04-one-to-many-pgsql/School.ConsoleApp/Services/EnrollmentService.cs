using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL;

namespace School.ConsoleApp.Services;

public class EnrollmentService
{
    private readonly DbContextOptions<SchoolContext> _options;

    public EnrollmentService(DbContextOptions<SchoolContext> options)
    {
        _options = options;
    }

    public void AllEnrollments()
    {
        Console.WriteLine("All Enrollments");
        // TODO Lab 5: All Enrollments
    }

    public void StudentsAndCourses()
    {
        Console.WriteLine("Students And Courses");
        // TODO Lab 13-3: Students And Courses
    }
}