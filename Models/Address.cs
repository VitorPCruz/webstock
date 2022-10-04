using System.ComponentModel.DataAnnotations;
using WebStock.Models.Entities;

namespace WebStock.Models;

public class Address : Entity
{
    // public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(200, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 3)]
    public string Street { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(5, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 1)]
    public string Number { get; set; }

    [StringLength(300, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 4)]
    public string? Complement { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(100, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 3)]
    public string Neighborhood { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(8, ErrorMessage = "The field '{0}' must be {1} characters")]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(50, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 3)]
    public string City { get; set; }
    
    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(100, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 3)]
    public string State { get; set; }
}