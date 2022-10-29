using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStock.Models.Entities;

namespace WebStock.Models;

public class Product : Entity
{
    [DisplayName("Fornecedor")]
    [ForeignKey("Supplier")]
    public Guid? SupplierId { get; set; }

    [DisplayName("Categoria")]
    [ForeignKey("Category")]
    public Guid? CategoryId { get; set; }

    [DisplayName("Nome")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [StringLength(100, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
    public string Name { get; set; }

    [DisplayName("Código de Barras")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [StringLength(35, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
    public string CodeBar { get; set; }

    [DisplayName("Código do Produto")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [StringLength(50, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.", MinimumLength = 1)]
    public string ProductCode { get; set; }

    [DisplayName("Descrição")]
    [StringLength(1000, ErrorMessage = "O campo '{0}' deve ter até {1} caracteres.")]
    public string? Description { get; set; }

    [DisplayName("Ativo?")]
    public bool Active { get; set; }

    [DisplayName("Quantidade")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.")]
    public int Quantity { get; set; }
    
    public Supplier? Supplier { get; set; }
    public Category? Category { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Product product &&
               Id.Equals(product.Id) &&
               EqualityComparer<Guid?>.Default.Equals(SupplierId, product.SupplierId) &&
               EqualityComparer<Guid?>.Default.Equals(CategoryId, product.CategoryId) &&
               Name == product.Name &&
               CodeBar == product.CodeBar &&
               ProductCode == product.ProductCode &&
               Description == product.Description &&
               Active == product.Active &&
               Quantity == product.Quantity;
    }
}
