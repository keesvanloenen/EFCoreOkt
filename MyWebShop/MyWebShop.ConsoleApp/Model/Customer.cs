using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebShop.ConsoleApp.Model;

//[PrimaryKey(nameof(Name), "Ander")]
public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "DateTime2(0)")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
