#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    [MaxLength(45, ErrorMessage = "Name must be 45 characters or less")]
    public string Name { get; set; }

    [Required]
    [MaxLength(45, ErrorMessage = "Chef must be 45 characters or less")]
    public string Chef { get; set; }

    [Required]
    public int? Tastiness { get; set; }

    [Required]
    [Range(1, 10000)]
    public int? Calories { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}