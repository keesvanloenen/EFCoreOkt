using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.Services;

public class PersonService
{
    private readonly DbContextOptions<SchoolContext> _options;

    public PersonService(DbContextOptions<SchoolContext> options)
    {
        _options = options;
    }

    public void AllPeople()
    {
        Console.WriteLine("All People Paginated");
        // TODO Lab 8: Pagination

        int pageSize = 5;
        int firstPersonId = 0;
        int lastPersonId = 0;

        var people = Next(lastPersonId, pageSize);

        ConsoleKeyInfo key;
        while (true)
        {
            firstPersonId = people.FirstOrDefault()?.Id ?? 0;
            lastPersonId = people.LastOrDefault()?.Id ?? 0;

            Console.Clear();

            foreach (var person in people)
            {
                Console.WriteLine($"{person.Id} - {person.FirstName} {person.LastName}");
            }
            Console.WriteLine("(P)revious  (N)ext  (Q)uit");

            key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case 'n' or 'N':
                    people = Next(lastPersonId, pageSize);
                    break;
                case 'p' or 'P':
                    people = Previous(firstPersonId, pageSize);
                    break;
                case 'q' or 'q':
                    return;
                default:
                    break;
            }
        }
    }

    public List<Person> Previous(int firstPersonId, int pageSize)
    {
        using var context = new SchoolContext(_options);
        return context.People
            .Where(p => p.Id < firstPersonId)
            .OrderByDescending(p => p.Id)
            .Take(pageSize)
            .OrderBy(p => p.Id)
            .ToList();
    }

    public List<Person> Next(int lastPersonId, int pageSize)
    {
        using var context = new SchoolContext(_options);
        return context.People
            .Where(p => p.Id > lastPersonId)
            .Take(pageSize)
            .ToList();
    }


    //public void AllPeople()
    //{
    //    Console.WriteLine("All People");
    //    // TODO Lab 1: Display All People

    //    using var context = new SchoolContext(_options);

    //    var people = context.People;

    //    foreach (var person in people)
    //    {
    //        Console.WriteLine($"{person.Id,2} - {person.FirstName} {person.LastName}");
    //    }
    //}

    public void Students()
    {
        Console.WriteLine("Students");
        // TODO Lab 3-1-6: Students
        // TODO Lab 6-1: Students Ordered By Enrollment Date

        using var context = new SchoolContext(_options);
        var students = context.Students
            .AsNoTracking()
            .OrderBy(s => s.EnrollmentDate);

        Console.WriteLine($"{"Student",-22} EnrollmentDate");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.FirstName + " " + student.LastName,-22} {student.EnrollmentDate}");
        }

    }
    public void Instructors()
    {
        Console.WriteLine("Instructors");
        // TODO Lab 3-1-6: Instructors

        using var context = new SchoolContext(_options);
        var instructors = context.Instructors;

        foreach (var instructor in instructors)
        {
            Console.WriteLine($"{instructor.Id} - {instructor.FirstName} {instructor.LastName}, HireDate: {instructor.HireDate}");
        }
    }

    public void DepartmentAdministrators()
    {
        Console.WriteLine("Department Administrators");
        // TODO Lab 3-1-6: Department Administrators

        using var context = new SchoolContext(_options);

        var departmentAdministrators = context.DepartmentAdministrators;

        foreach (var admin in departmentAdministrators)
        {
            Console.WriteLine($"{admin.Id} - {admin.FirstName} {admin.LastName}, HireDate: {admin.HireDate}");
        }
    }

    public void FirstInstructorHired()
    {
        Console.WriteLine("First Instructor Hired");
        // TODO Lab 6-2: First Instructor Hired

        using var context = new SchoolContext(_options);

        var instructor = context.Instructors
            .AsNoTracking()
            .OrderBy(i => i.HireDate)
            .First();

        Console.WriteLine($"{instructor.FirstName} {instructor.LastName} ({instructor.HireDate})");
    }

    public void AverageGradePerStudent()
    {
        Console.WriteLine("Average Grade Per Student");
        // TODO Lab 6-3: Average Grade Per StudentId
        // TODO Lab 7-2: Average Grade Per Student

        using var context = new SchoolContext(_options);

        var students = context.Students
            .AsNoTracking()
            .Include(s => s.Enrollments)
            .Select(s => new
            {
                Student = $"{s.FirstName} {s.LastName}",
                AverageGrade = s.Enrollments.Where(e => e.Grade != null).Average(e => e.Grade)
            })
            .Where(r => r.AverageGrade != null);


        Console.WriteLine("Student                   Grade");

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Student,-25} {s.AverageGrade:N2}");
        }
    }

    public void InstructorDetails()
    {
        Console.WriteLine("Instructor Details");
        // TODO Lab 7-4: Instructor Details By Id

        using var context = new SchoolContext(_options);

        int id = GetInstructorId();
        var instructor = context.Instructors.Find(id);

        if (instructor == null)
        {
            Console.WriteLine($"No instructor found with id {id}.");
            return;
        }

        // Explicit load Courses
        context.Entry(instructor).Collection(i => i.Courses).Load();

        Console.WriteLine($"Instructor {instructor.FirstName} {instructor.LastName}");
        Console.WriteLine("Teaching:");

        if (instructor.Courses.Count == 0)
        {
            Console.WriteLine("\t- No courses assigned");
            return;
        }

        foreach (var course in instructor.Courses)
        {
            Console.WriteLine($"\t- {course.Title}");
        }
    }

    public void DeleteDepartmentAdministratorKeepDepartments()
    {
        Console.WriteLine("Delete Administrator, but keep Departments");
        // TODO Lab 10-3: Delete Department Administrator, Keep Departments
    }

    public void DeleteDepartmentAdministratorWithDepartments()
    {
        Console.WriteLine("Delete Administrator along with the Departments");
        // TODO Lab 10-4: Delete Department Administrator Along With Departments
    }

    public void Best5Students()
    {
        Console.WriteLine("Best 5 Students");
        // TODO Lab 13-1: Best 5 Students
    }

    private static int GetInstructorId()
    {
        while (true)
        {
            Console.Write("Enter instructor id: ");
            string? input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int id) && id > 0)
            {
                return id;
            }
            Console.WriteLine("Invalid instructor id. Please enter a positive integer.");
        }
    }
}