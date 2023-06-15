#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    [Required]
    public int DishId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required] 
    public string Chef { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage =("Tastiness must be between 1 and 5!"))]
    public int Tastiness{ get; set; }
    [Required]
    [Range(0, 100, ErrorMessage = ("Calories must be greater than 0!"))]
    public int Calories {get;set;}
    [Required]
    public string Description {get;set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
                
