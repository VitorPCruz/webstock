using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStock.Models.Entities;


namespace WebStock.Models;

public class Supplier : Entity
{
    [DisplayName("Nome")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [StringLength(200, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
    public string Name { get; set; }

    [DisplayName("CPF/CNPJ")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [StringLength(18, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.", MinimumLength = 14)]
    public string Document { get; set; }

    [DisplayName("E-mail")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [EmailAddress(ErrorMessage = "Insira um e-mail válido")]
    [StringLength(100, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.", MinimumLength = 5)]
    public string Email { get; set; }

    [DisplayName("Tipo de Fornecedor")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    public SupplierType SupplierType { get; set; }

    [DisplayName("Ativo?")]
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
