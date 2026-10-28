using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebShop.ConsoleApp.DAL;
using MyWebShop.ConsoleApp.Model;

namespace MyWebShop.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<WebShopDbContext>()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyWebShop;ConnectRetryCount=0")
            .Options;

        using var context = new WebShopDbContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var customer1 = new Customer { Name = "Mo" };
        var customer2 = new Customer { Name = "Ab" };
        var customer3 = new Customer { Name = "Bo" };

        context.AddRange([customer1, customer2, customer3]);
        context.SaveChanges();

        ShowCustomers(options);
    }

    private static void ShowCustomers(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var customers = context.Customers;

        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.Id} - {customer.Name}");
        }
    }
}
