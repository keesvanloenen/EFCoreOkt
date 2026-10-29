using Microsoft.EntityFrameworkCore;
using School.Library.DAL;
using School.Library.Models;

namespace School.Library.Services;

public class DepartmentService
{
    private readonly DbContextOptions<SchoolContext> _options;

    public DepartmentService(DbContextOptions<SchoolContext> options)
    {
        _options = options;
    }

    public void AllDepartments()
    {
        Console.WriteLine("All Departments");
        // TODO Lab 2-4: All Departments
        // TODO Lab 4: Include AdministratorId
        // TODO Lab 7-1: All Departments Full Details

        using var context = new SchoolContext(_options);

        var departments = context.Departments
            .AsNoTracking()
            .Include(d => d.Administrator)
            .ThenInclude(a => a!.Courses);

        foreach (var d in departments)
        {
            Console.WriteLine($"Department name: {d.Name}, Budget: {d.Budget:N2}, Administrator: {d.Administrator?.FirstName} {d.Administrator?.LastName}");

            foreach (var c in d.Administrator?.Courses ?? [])
            {
                Console.WriteLine($"\t- {c.Title}");
            }
        }
    }

    public void AddUpdateDeleteDepartment()
    {
        AddNewDepartment();
        UpdateBudgetLastAddedDepartment();
        DeleteLastAddedDepartment();
    }

    private void AddNewDepartment()
    {
        Console.WriteLine("Add New Department");
        // TODO Lab 9-1: Add New Department
        // TODO Lab 19: Display Change Tracker Info

        Console.Write("Department Name: ");
        var name = Console.ReadLine();
        if (name == null)
        {
            Console.WriteLine($"Not a valid name: {name}");
            return;
        }

        Console.Write("Department Budget: ");
        var input = Console.ReadLine();
        if (!decimal.TryParse(input, out var budget))
        {
            Console.WriteLine($"Not a valid budget: {input}");
            return;
        }

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var department = new Department { Name = name, Budget = budget, StartDate = startDate };
        using var context = new SchoolContext(_options);

        context.Add(department);

        context.SaveChanges();

        Console.WriteLine($"Department '{department.Name}' added with ID {department.Id}");
        Console.WriteLine();
    }

    private void UpdateBudgetLastAddedDepartment()
    {
        Console.WriteLine("Update Budget Last Added Department");
        // TODO Lab 9-2: Update Budget Last Added Department
        // TODO Lab 19: Display Change Tracker Info

        using var context = new SchoolContext(_options);

        var department = context.Departments.OrderBy(d => d.Id).LastOrDefault();

        if (department == null)
        {
            Console.WriteLine("No departments found");
            return;
        }

        Console.Write($"New budget for department '{department.Name}': ");
        var input = Console.ReadLine();
        if (!decimal.TryParse(input, out var budget))
        {
            Console.WriteLine($"Not a valid budget: {input}");
            return;
        }

        department.Budget = budget;

        context.SaveChanges();

        Console.WriteLine($"Department '{department.Name}' has got a new budget: {department.Budget}");
        Console.WriteLine();
    }

    private void DeleteLastAddedDepartment()
    {
        Console.WriteLine("Delete Last Added Department");
        // TODO Lab 9-3: Delete Last Added Department
        // TODO Lab 19: Display Change Tracker Info

        using var context = new SchoolContext(_options);

        var department = context.Departments.OrderBy(d => d.Id).LastOrDefault();

        if (department == null)
        {
            Console.WriteLine("No departments found");
            return;
        }

        context.Remove(department);

        context.SaveChanges();

        Console.WriteLine($"Department '{department.Name}' has been deleted");
    }

    public void UpdateDepartment()
    {
        Console.WriteLine("Update Department");
        // TODO Lab 11: Update Department

        Console.Write("Enter Department Id: ");
        var departmentId = int.Parse(Console.ReadLine()!);

        using var context = new SchoolContext(_options);

        var department = context.Departments.Find(departmentId);

        if (department == null)
        {
            Console.WriteLine("Invalid Department Id");
            return;
        }

        Console.WriteLine($"Id = {department.Id}, Name = {department.Name}, Budget = {department.Budget}, Startdate = {department.StartDate}, AdministratorId = {department.AdministratorId}");

        Console.Write("Enter the new name: ");
        var newName = Console.ReadLine()!;

        Console.Write("Enter the new budget: ");
        var newBudget = decimal.Parse(Console.ReadLine()!);

        Console.Write("Enter the new start date: ");
        var newStartDate = DateOnly.Parse(Console.ReadLine()!);

        department.Name = newName;
        department.Budget = newBudget;
        department.StartDate = newStartDate;

        // Simulate a change in the database by another user
        context.Database.ExecuteSql($"Update school.\"Departments\" SET \"Name\" = 'Something different' WHERE \"Id\" = {departmentId}");

        try
        {
            context.SaveChanges();
            Console.WriteLine($"Department updated:");
            Console.WriteLine($"Id = {department.Id}, Name = {department.Name}, Budget = {department.Budget}, Startdate = {department.StartDate}, AdministratorId = {department.AdministratorId}");

        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                if (entry.Entity is Department)
                {
                    var databaseValues = entry.GetDatabaseValues();

                    // Database Wins
                    //entry.CurrentValues.SetValues(databaseValues!);
                    //Console.WriteLine("The record has been modified by another user. Please try again!");

                    // Client Wins
                    entry.OriginalValues.SetValues(databaseValues!);
                    context.SaveChanges();
                }
            }
        }
    }

    public void GetDepartmentsWithEnrollmentsAbove()
    {
        Console.WriteLine("Get Departments With Enrollments Above");
        // TODO Lab 13-2: Get Departments With Enrollments Above

        var numberOfEnrollments = 5;

        using var context = new SchoolContext(_options);

        var departments = context.Departments
            .FromSql($"SELECT * FROM school.\"GetDepartmentsWithEnrollmentsAbove\"({numberOfEnrollments})")
            .ToList();

        Console.WriteLine($"Show Departments with more than {numberOfEnrollments} enrollments:");

        foreach (var department in departments)
        {
            Console.WriteLine($"  - {department.Name}");
        }
    }
}