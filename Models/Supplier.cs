using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStock.Models.Entities;

namespace WebStock.Models;

public class Supplier : Entity
{
    // public Guid AddressId { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(200, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 3)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(14, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 11)]
    public string Document { get; set; }

    [DisplayName("E-mail")]
    [Required(ErrorMessage = "The field '{0}' is required")]
    [EmailAddress(ErrorMessage = "Insert a valid e-mail")]
    [StringLength(100, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 5)]
    public string Email { get; set; }

    [DisplayName("Type")]
    [Required(ErrorMessage = "The field '{0}' is required")]
    public SupplierType SupplierType { get; set; }

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
    [StringLength(8, ErrorMessage = "The field '{0}' must be {1} characters", MinimumLength = 8)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(50, ErrorMessage = "The field '{0}' must be between {2} and {1} characters", MinimumLength = 3)]
    public string City { get; set; }

    [Required(ErrorMessage = "The field '{0}' is required")]
    [StringLength(2, ErrorMessage = "The field '{0}' must be {1} characters")]
    public string State { get; set; }
    public IEnumerable<Product>? Products { get; set; }

    //public DateTime CreatedAt { get; set; }
    //public DateTime ModifiedAt { get; set; }

}
