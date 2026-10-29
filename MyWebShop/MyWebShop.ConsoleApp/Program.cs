
using Microsoft.EntityFrameworkCore;
using MyWebShop.ConsoleApp.DAL;
using MyWebShop.ConsoleApp.Model;
using System.Runtime.CompilerServices;
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


        BuildDatabase(options);
        DataSeed(options);
        
        ShowCustomers(options);
        //ShowCustomer(options);
        //ShowOrders(options);
        //ShowCustomersAndOrders(options);
    }

    private static void ShowCustomers(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var customers = context.Customers
            .AsNoTracking()
            //.AsEnumerable()
            //.Where(c => HeeftDeNaamEenKlinker(c.Name))
            .Where(c => c.Name.Contains('e'))
            //.OrderByDescending(c => c.CreditLimit)
            .Select(c => new { c.Name, KlinkerNaam = HeeftDeNaamEenKlinker(c.Name) });

        Console.WriteLine(customers.ToQueryString());
        
        foreach (var customer in customers)
        {
            //Console.WriteLine($"{customer.Id} - {customer.Name} (gemaakt op: {customer.CreatedAt:dd MM yyyy})");
            Console.WriteLine($"{customer.Name} - {customer.KlinkerNaam}");
        }
    }

    private static bool HeeftDeNaamEenKlinker(string input)
    {
        char firstLetter = char.ToLower(input[0]);
        return firstLetter is 'o' or 'i' or 'e' or 'a'; 
    }

    private static void DataSeed(DbContextOptions<WebShopDbContext> options)
    {
        var customer1 = new Customer { Name = "Mo" };
        var customer2 = new Customer { Name = "Ab" };
        var customer3 = new Customer { Name = "Bo" };
        var customer4 = new Customer { Name = "Cas" };
        var customer5 = new Customer { Name = "De" };
        var customer6 = new Customer { Name = "Ferdinand" };

        using var context = new WebShopDbContext(options);

        context.AddRange([customer1, customer2, customer3, customer4, customer5, customer6]);
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
    }

    private static void BuildDatabase(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
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

    private static void ShowCustomer(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        //var customer = context.Customers.FirstOrDefault(c => c.Id == 3);
        var customer = context.Customers.Find(3);

        if (customer is null)
        {
            Console.WriteLine("Pech");
            return;
        }

        Console.WriteLine(customer.Name);

    }
}
