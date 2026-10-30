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
            //.UseSqlServer(@"Server=127.0.0.1,1433;Database=MijnWebshop;User=sa;Password=geheim;TrustServerCertificate=True")
            .Options;


        BuildDatabase(options);
        DataSeed(options);

        //ShowCustomers(options);
        //ShowCustomer(options);
        //ShowOrders(options);
        //ShowCustomersAndOrders(options);
        //ExplicitLoading(options);
        //ProjectionAlsReadOnlyHetGevalIs(options);

        //AddCustomerAndOrders(options);
        //Concurrency(options);
        ExecuteSql(options);
        //FromSql(options);
        FromSqlStoredProcedureAanroepen(options);
    }

    private static void FromSqlStoredProcedureAanroepen(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var id = 2;
        var lastOrder = context.Orders
            .FromSql($"EXEC dbo.ShowLastOrderForCustomer {id}")
            .AsEnumerable()
            .FirstOrDefault();

        Console.WriteLine(lastOrder?.TotalAmount.ToString() ?? "niets gevonden");
    }

    private static void FromSql(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var kolomNaam = "name = 'x' OR 1 = 1 --";
        var filter = "%o";

        var customers = context.Customers
            .FromSqlRaw($"SELECT * FROM customers WHERE {kolomNaam} LIKE '{filter}'");

        Console.WriteLine(customers.ToQueryString());

        Console.WriteLine(string.Join(", ", customers.Select(c => c.Name)));


    }

    private static void ExecuteSql(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);
        context.Database.ExecuteSql(
            $"""
            CREATE OR ALTER PROCEDURE dbo.ShowLastOrderForCustomer
            
                 @customerId As int
               AS
               BEGIN
                    SELECT TOP 1 *
                    FROM Orders AS o
                    WHERE o.CustomerId = @customerId
                    ORDER BY OrderDate DESC;
            END
            """
            );
    }

    //private static void FromSql(DbContextOptions<WebShopDbContext> options)
    //{
    //    using var context = new WebShopDbContext(options);

    //    var customers = context.Customers
    //        .FromSql()
    //}

    private static void Concurrency(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        // User A
        var customer = context.Customers.Find(1)!;
        Console.WriteLine($"User A loads: CreditLimit: {customer.CreditLimit}");

        // User B
        context.Customers
            .Where(c => c.Id == 1)
            .ExecuteUpdate(setters => setters.SetProperty(c => c.CreditLimit, 2000m));
        
        Console.WriteLine("User B saved: Credit: 2000");

        // User A
        customer.CreditLimit = 1500m;

        try
        {
            context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine("💀 Conflict");

            foreach(var entry in ex.Entries)
            {
                if (entry.Entity is Customer conflictedCustomer)
                {

                    // lees db waardes uit
                    var dbValues = entry.GetDatabaseValues();

                    // database WINS scenario
                    //entry.CurrentValues.SetValues(dbValues!);
                    //Console.WriteLine("Changes discarded." );

                    // client WINS scenario
                    entry.OriginalValues.SetValues(dbValues!);
                    context.SaveChanges();
                    Console.WriteLine("Client wins: 1500 euro");

                }
            }
        }
    }

    private static void AddCustomerAndOrders(DbContextOptions<WebShopDbContext> options)
    {
        var customerId = 0;

        var newCustomer = new Customer
        {
            Name = "Gijs",
            PhoneNumber = "1234567890",
            CreditLimit = 100.00m,
            CreatedAt = DateTime.Now,
        };

        newCustomer.Orders.AddRange([
            new Order
            {
                OrderDate = DateTime.Now.AddDays(-2),
                TotalAmount = 149.50m
            },
            new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = 149.50m
            },
        ]);

        using (var context = new WebShopDbContext(options))
        {
            context.Customers.Add(newCustomer);
            context.SaveChanges();
            customerId = newCustomer.Id;

        }   // hier stopt de eerste context (dus connectie)

        // voor nu de controle context (een 2de frisse context)
        using (var context = new WebShopDbContext(options))
        {
            var customer = context.Customers.Find(customerId);

            if (customer is null)
            {
                Console.WriteLine("Something horribly went wrong");
            }
        }
    }

    private static void ProjectionAlsReadOnlyHetGevalIs(DbContextOptions<WebShopDbContext> options)
    {
        using var context = new WebShopDbContext(options);

        var customersTeVeel = context.Customers.Include(c => c.Orders);
        Console.WriteLine(customersTeVeel.ToQueryString());

        var customers = context.Customers
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    OrderInfo = c.Orders.Select(o => new
                    {
                        o.OrderDate,
                        o.TotalAmount
                    })
                });

        Console.WriteLine(customers.ToQueryString());
                

        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.Id} {customer.Name}");

            foreach(var order in customer.OrderInfo)
            {
                Console.WriteLine($"{order.OrderDate,11:yyyymmdd} {order.TotalAmount,5:C}");
            }

        }
    }

    private static void ExplicitLoading(DbContextOptions<WebShopDbContext> options)
    {
        Console.Write("Enter the customerid: ");
        string input = Console.ReadLine() ?? string.Empty;

        var customerId = int.Parse(input);

        using var context = new WebShopDbContext(options);

        Customer? customer = context.Customers.Find(customerId);

        if (customer == null)
        {
            Console.WriteLine("Jammer");
            return;
        }

        // 🎺 tadaa explicit loading
        context.Entry(customer)
            .Collection(c => c.Orders)      // bij iets enkelvouds moet het .Reference zijn ipv .Collection
            .Load();

        Console.WriteLine($"De orders voor klant: {customerId}");

        foreach (var order in customer.Orders)
        {
            Console.WriteLine($"\t - {order.Id} datum: {order.OrderDate}");
        }
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
        var customer1 = new Customer { Name = "Ab", PhoneNumber = "061111111", CreditLimit = 2000.00m };
        var customer2 = new Customer { Name = "Bo", PhoneNumber = "062222222", CreditLimit = 2000.00m };
        var customer3 = new Customer { Name = "Cas", PhoneNumber = "063333333", CreditLimit = 180000.00m };
        var customer4 = new Customer { Name = "De", PhoneNumber = "064444444", CreditLimit = 1800.00m };
        var customer5 = new Customer { Name = "Ferdinand", PhoneNumber = "065555555", CreditLimit = 1600.00m };
        var customer6 = new Customer { Name = "Hans", PhoneNumber = "066666666", CreditLimit = 1600.00m };

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
