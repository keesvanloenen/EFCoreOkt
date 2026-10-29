namespace MyWebShop.ConsoleApp.Model;

public class ProductCategory
{
    public long Category_Id { get; set; }
    public Category Category { get; set; } = null!;
    public long Product_Id { get; set; }
    public Product Product { get; set; } = null!;
    public DateOnly DateAdded { get; set; }
}
