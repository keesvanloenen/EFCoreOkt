using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL;

namespace School.ConsoleApp.Services;

public class CourseService
{
    private readonly DbContextOptions<SchoolContext> _options;

    public CourseService(DbContextOptions<SchoolContext> options)
    {
        _options = options;
    }

    public void AllCourses()
    {
        Console.WriteLine("All Courses");
        // TODO Lab 2-4: All Courses

        using var context = new SchoolContext(_options);
        var courses = context.Courses;

        foreach (var course in courses)
        {
            Console.WriteLine($"{course.Id} - {course.Title}, Credits = {course.Credits}");
        }
    }

    public void OnlineCourses()
    {
        Console.WriteLine("Online Courses");
        // TODO Lab 3-2-6: Online Courses

        using var context = new SchoolContext(_options);

        var onlineCourses = context.OnlineCourses;

        foreach (var course in onlineCourses)
        {
            Console.WriteLine($"{course.Id} - {course.Title}, Credits = {course.Credits}, Url = {course.Url}");
        }
    }

    public void OnsiteCourses()
    {
        Console.WriteLine("Onsite Courses");
        // TODO Lab 3-2-6: Onsite Courses

        using var context = new SchoolContext(_options);

        var onsiteCourses = context.OnsiteCourses;

        foreach (var course in onsiteCourses)
        {
            Console.WriteLine($"{course.Id} - {course.Title}, Credits = {course.Credits}, Location = {course.Location}, Time = {course.Time}");
        }
    }

    public void LowestGradePerCourse()
    {
        Console.WriteLine("Lowest Grade Per Course");
        // TODO Lab 6-4: Lowest Grade Per CourseId
        // TODO Lab 7-3: Lowest Grade Per Course

        using var context = new SchoolContext(_options);

        var courses = context.Courses
            .AsNoTracking()
            .Include(c => c.Enrollments)
            .Select(c => new
            {
                Course = c.Title,
                LowestGrade = c.Enrollments.Where(e => e.Grade != null).Min(e => e.Grade)
            })
            .Where(r => r.LowestGrade != null);

        foreach (var c in courses)
        {
            Console.WriteLine($"{c.Course}: {c.LowestGrade:N2}");
        }
    }

    public void IncreaseCreditsOfCoursesInSelectedDepartment()
    {
        Console.WriteLine("Increase Credits Of Courses In Selected Department By 10");
        // TODO Lab 12: Increase Credits Of Courses In Selected Department

        Console.Write("Provide Department Id: ");
        var departmentId = int.Parse(Console.ReadLine()!);

        using var context = new SchoolContext(_options);

        var numberOfUpdatedCourses = context.Courses
            .Where(c => c.DepartmentId == departmentId)
            .ExecuteUpdate(setters => setters.SetProperty(c => c.Credits, c => c.Credits + 10));

        if (numberOfUpdatedCourses == 0)
        {
            Console.WriteLine($"No Courses found for department with id {departmentId}");
            return;
        }

        Console.WriteLine($"Credits increased for {numberOfUpdatedCourses} courses for department {departmentId}");
    }

    public void CourseGradeStandardDeviations()
    {
        Console.WriteLine("Course Grade Standard Deviations");
        // TODO Lab 14: Course Grade Standard Deviations
    }
}