namespace School.ConsoleApp.Services;

public class PersonService
{
    public void AllPeople()
    {
        Console.WriteLine("All People");
        // TODO Lab 1: Display All People
        // TODO Lab 8: Pagination
    }

    public void Students()
    {
        Console.WriteLine("Students");
        // TODO Lab 3-1-6: Students
        // TODO Lab 6-1: Students Ordered By Enrollment Date
    }
    public void Instructors()
    {
        Console.WriteLine("Instructors");
        // TODO Lab 3-1-6: Instructors
    }

    public void DepartmentAdministrators()
    {
        Console.WriteLine("Department Administrators");
        // TODO Lab 3-1-6: Department Administrators
    }

    public void FirstInstructorHired()
    {
        Console.WriteLine("First Instructor Hired");
        // TODO Lab 6-2: First Instructor Hired
    }

    public void AverageGradePerStudent()
    {
        Console.WriteLine("Average Grade Per Student");
        // TODO Lab 6-3: Average Grade Per StudentId
        // TODO Lab 7-2: Average Grade Per Student
    }

    public void InstructorDetails()
    {
        Console.WriteLine("Instructor Details");
        // TODO Lab 7-4: Instructor Details By Id
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