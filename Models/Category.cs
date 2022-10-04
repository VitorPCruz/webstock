using System.ComponentModel.DataAnnotations;
using WebStock.Models.Entities;

namespace WebStock.Models;

public class Category : Entity
{
    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(100, ErrorMessage = "The field '{0}' must be between {2} and {1} characters",
        MinimumLength = 2)]
    public string Name { get; set; }
}
