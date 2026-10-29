using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.DAL;

namespace School.ConsoleApp.Services;

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

        var departments = context.Departments;

        foreach (var department in departments)
        {
            Console.WriteLine($"{department.Id} - {department.Name}, Budget = {department.Budget}, Startdate = {department.StartDate}, AdministratorId = {department.AdministratorId}");
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
    }

    private void UpdateBudgetLastAddedDepartment()
    {
        Console.WriteLine("Update Budget Last Added Department");
        // TODO Lab 9-2: Update Budget Last Added Department
        // TODO Lab 19: Display Change Tracker Info
    }

    private void DeleteLastAddedDepartment()
    {
        Console.WriteLine("Delete Last Added Department");
        // TODO Lab 9-3: Delete Last Added Department
        // TODO Lab 19: Display Change Tracker Info
    }

    public void UpdateDepartment()
    {
        Console.WriteLine("Update Department");
        // TODO Lab 11: Update Department
    }

    public void GetDepartmentsWithEnrollmentsAbove()
    {
        Console.WriteLine("Get Departments With Enrollments Above");
        // TODO Lab 13-2: Get Departments With Enrollments Above
    }
}