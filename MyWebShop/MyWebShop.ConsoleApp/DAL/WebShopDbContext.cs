using Microsoft.EntityFrameworkCore;
using MyWebShop.ConsoleApp.DAL.Configurations;
using MyWebShop.ConsoleApp.Model;

namespace MyWebShop.ConsoleApp.DAL;

public class WebShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder builder)
    //{
    //    builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyWebShop;ConnectRetryCount=0");
    //}

    public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}