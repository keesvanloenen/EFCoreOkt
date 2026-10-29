using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebShop.ConsoleApp.DAL;
using MyWebShop.ConsoleApp.Model;
using System.Text;

namespace MyWebShop.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

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

        var order1 = new Order 
        { 
            OrderDate = DateTime.Now.AddDays(-3),
            TotalAmount = 10000m,
            Customer = customer1,
        };

        var order2 = new Order
        {
            OrderDate = DateTime.Now.AddDays(-2),
            TotalAmount = 50m,
            Customer = customer1,
        };
        var order3 = new Order
        {
            OrderDate = DateTime.Now,
            TotalAmount = 100.99m,
            Customer = customer2,
        };

        context.AddRange([order1, order2, order3]);
        context.SaveChanges();

        //ShowCustomers(options);
        //ShowOrders(options);
        ShowCustomersAndOrders(options);
    }

    private static void ShowCustomersAndOrders(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var orders = context.Orders
            .Include(o => o.Customer);

        foreach (var order in orders)
        {
            Console.WriteLine($"{order.Id} - {order.OrderDate:dd MM}: {order.TotalAmount:C} {order.Customer.Name}");
        }

    }

    private static void ShowOrders(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var orders = context.Orders;

        foreach (var order in orders)
        {
            Console.WriteLine($"{order.Id} - {order.OrderDate:dd MM}: {order.TotalAmount:C}");
        }
    }

    private static void ShowCustomers(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var customers = context.Customers;

        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.Id} - {customer.Name} (gemaakt op: {customer.CreatedAt:dd MM yyyy})");
        }
    }
}
