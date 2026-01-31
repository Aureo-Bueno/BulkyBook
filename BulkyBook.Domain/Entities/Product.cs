using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Value { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
