using System.ComponentModel.DataAnnotations;

namespace WebStock.Models;

public enum SupplierType
{
    [Display(Name = "Pessoa Física")]
    PhysicalPerson=1,

    [Display(Name = "Pessoa Jurídica")]
    LegalPerson = 2
}
