using System.ComponentModel.DataAnnotations;

namespace WebStock.Models;

public enum SupplierType
{
    [Display(Name = "Physical Person")]
    PhysicalPerson=1,

    [Display(Name = "Legal Person")]
    LegalPerson = 2
}
