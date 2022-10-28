using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStock.Models.Entities;

namespace WebStock.Models;

public class Category : Entity
{
    [DisplayName("Nome")]
    [Required(ErrorMessage = "O campo '{0}' é necessário.")]
    [StringLength(100, ErrorMessage = "O campo '{0}' precisa ter entre {2} e {1} caracteres.",
        MinimumLength = 2)]
    public string Name { get; set; }

    [DisplayName("Ativo?")]
    public bool Active { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Category category &&
               Id.Equals(category.Id) &&
               Name == category.Name &&
               Active == category.Active;
    }
}
