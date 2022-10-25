using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStock.Models.Entities;


namespace WebStock.Models;

public class Supplier : Entity
{
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

    [DisplayName("Supplier Type")]
    [Required(ErrorMessage = "The field '{0}' is required")]
    public SupplierType SupplierType { get; set; }

    [DisplayName("Active?")]
    public bool Active { get; set; }

    public IEnumerable<Product> Products { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Supplier supplier &&
               Id.Equals(supplier.Id) &&
               Name == supplier.Name &&
               Document == supplier.Document &&
               Email == supplier.Email &&
               SupplierType == supplier.SupplierType &&
               Active == supplier.Active &&
               EqualityComparer<IEnumerable<Product>>.Default.Equals(Products, supplier.Products);
    }
}
