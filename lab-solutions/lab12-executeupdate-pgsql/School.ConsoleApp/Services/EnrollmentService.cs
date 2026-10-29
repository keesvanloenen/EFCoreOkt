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

        using var context = new SchoolContext(_options);

        var enrollments = context.Enrollments;

        Console.WriteLine("StudentId CourseId");
        foreach (var enrollment in enrollments)
        {
            Console.WriteLine($"{enrollment.StudentId, -9} {enrollment.CourseId}");
        }
    }

    public void StudentsAndCourses()
    {
        Console.WriteLine("Students And Courses");
        // TODO Lab 13-3: Students And Courses
    }
}