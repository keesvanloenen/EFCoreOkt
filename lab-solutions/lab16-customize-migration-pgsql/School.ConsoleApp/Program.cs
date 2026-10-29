namespace School.Library;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Library.DAL;
using School.Library.Menu;
using School.Library.Services;
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
        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();
        //SchoolInitializer.Seed(options);

        var personService = new PersonService(options);
        var courseService = new CourseService(options);
        var departmentService = new DepartmentService(options);
        var enrollmentService = new EnrollmentService(options);

        var menuManager = new MenuManager(personService, courseService, departmentService, enrollmentService);

        menuManager.ShowMainMenu();
    }
}