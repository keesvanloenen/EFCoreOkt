namespace School.ConsoleApp.Services;

public class DepartmentService
{
    public void AllDepartments()
    {
        Console.WriteLine("All Departments");
        // TODO Lab 2-4: All Departments
        // TODO Lab 4: Include AdministratorId
        // TODO Lab 7-1: All Departments Full Details
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