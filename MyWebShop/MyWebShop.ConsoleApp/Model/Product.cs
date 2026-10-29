namespace MyWebShop.ConsoleApp.Model;

public class Product
{
    public long ProductId { get; set; }
    public required string Naam { get; set; }
    public decimal Prijs { get; set; }

    // Navigation
    public ICollection<ProductCategory> ProductCategorieen { get; set; } = [];
}
