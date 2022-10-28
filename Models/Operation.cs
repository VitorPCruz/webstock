using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebStock.Models;

public enum Operation
{
    [Display(Name = "Entrada")]
    Added=1,
    [Display(Name = "Saída")]
    Removed,
    [Display(Name = "Atualizou")]
    Updated
}