using Microsoft.EntityFrameworkCore;
using MyWebShop.ConsoleApp.DAL.Configurations;
using MyWebShop.ConsoleApp.Model;

namespace MyWebShop.ConsoleApp.DAL;

public class WebShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categorieen { get; set; }
    


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

        modelBuilder.Entity<ProductCategory>()
            //.HasKey(pc => pc.Product_Id)
            .HasKey(pc => new { pc.Product_Id, pc.Category_Id });

        // Category naar ProductCategory
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProductCategorieen)
            .HasForeignKey(pc => pc.Category_Id);

        // Product naar ProductCategory
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategorieen)
            .HasForeignKey(pc => pc.Product_Id);

        modelBuilder.Entity<ProductCategory>()
            .Property(pc => pc.DateAdded)
            .HasColumnType("Date")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();

    }
}