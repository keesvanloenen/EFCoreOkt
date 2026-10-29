using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL;
using School.ConsoleApp.ViewModels;

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

        using var context = new SchoolContext(_options);

        List<StudentWithCourse> studentsCourses = context.Database
            .SqlQuery<StudentWithCourse>(
            @$"SELECT 
                   p.FirstName, p.LastName, c.Title 
               FROM Enrollments AS e
               INNER JOIN Courses AS c
               ON e.CourseId = c.Id
               INNER JOIN People AS p
               ON e.StudentId = p.Id
               ORDER BY p.FirstName, c.Title"
            )
            .ToList();

        foreach (var sc in studentsCourses)
        {
            Console.WriteLine($"Student: {sc.FirstName} {sc.LastName} - Course: {sc.Title}");
        }
    }
}