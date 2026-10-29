namespace School.ConsoleApp;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.ConsoleApp.DAL;
using School.ConsoleApp.Menu;
using School.ConsoleApp.Services;
using System;

class Program
{
    static void Main()
    {
        Console.Title = "School System";


        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var configuration = builder.Build();

        var options = new DbContextOptionsBuilder<SchoolContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConn"))
            .Options;

        using var context = new SchoolContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        SchoolInitializer.Seed(options);

        var personService = new PersonService(options);
        var courseService = new CourseService(options);
        var departmentService = new DepartmentService(options);
        var enrollmentService = new EnrollmentService(options);

        var menuManager = new MenuManager(personService, courseService, departmentService, enrollmentService);

        menuManager.ShowMainMenu();
    }
}