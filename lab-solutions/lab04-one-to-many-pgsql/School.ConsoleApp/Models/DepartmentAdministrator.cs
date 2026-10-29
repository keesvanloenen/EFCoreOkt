namespace School.ConsoleApp.Models;

public class DepartmentAdministrator: Instructor
{
    public ICollection<Department> Departments { get; } = [];
}
