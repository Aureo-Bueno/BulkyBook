using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BulkyBookWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
        public DateTime CreatedAt  { get; set; } = DateTime.Now;
    }
}
