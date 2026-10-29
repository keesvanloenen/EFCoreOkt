namespace MyWebShop.ConsoleApp.Model;

public class Category
{
    public long Id { get; set; }
    public required string Titel { get; set; }


    // Navigation
    public ICollection<ProductCategory> ProductCategorieen { get; set; } = [];
}
