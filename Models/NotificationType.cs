using System.ComponentModel.DataAnnotations;

namespace WebStock.Models;

public enum NotificationType
{
    [Display(Name = "Erro")]
    Error,
    [Display(Name = "Successo")]
    Success,
    [Display(Name = "Aviso")]
    Warning,
    [Display(Name = "Info")]
    Info
}
