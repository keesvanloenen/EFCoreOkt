namespace School.ConsoleApp;

using School.ConsoleApp.Menu;
using School.ConsoleApp.Services;
using System;

class Program
{
    static void Main()
    {
        Console.Title = "School System";

        var personService = new PersonService();
        var courseService = new CourseService();
        var departmentService = new DepartmentService();
        var enrollmentService = new EnrollmentService();

        var menuManager = new MenuManager(personService, courseService, departmentService, enrollmentService);

        menuManager.ShowMainMenu();
    }
}