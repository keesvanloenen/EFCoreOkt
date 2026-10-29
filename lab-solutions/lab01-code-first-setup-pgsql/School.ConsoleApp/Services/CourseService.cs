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
    }

    public void OnlineCourses()
    {
        Console.WriteLine("Online Courses");
        // TODO Lab 3-2-6: Online Courses
    }

    public void OnsiteCourses()
    {
        Console.WriteLine("Onsite Courses");
        // TODO Lab 3-2-6: Onsite Courses
    }

    public void LowestGradePerCourse()
    {
        Console.WriteLine("Lowest Grade Per Course");
        // TODO Lab 6-4: Lowest Grade Per CourseId
        // TODO Lab 7-3: Lowest Grade Per Course
    }

    public void IncreaseCreditsOfCoursesInSelectedDepartment()
    {
        Console.WriteLine("Increase Credits Of Courses In Selected Department By 10");
        // TODO Lab 12: Increase Credits Of Courses In Selected Department
    }

    public void CourseGradeStandardDeviations()
    {
        Console.WriteLine("Course Grade Standard Deviations");
        // TODO Lab 14: Course Grade Standard Deviations
    }
}