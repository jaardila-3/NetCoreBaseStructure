using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Areas.Account.Models;

public class RoleViewModel
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "El {0} es obligatorio")]
    [StringLength(30, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 4)]
    [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "El {0} solo puede contener letras, números y guiones.")]
    [Display(Name = "Nombre del Rol")]
    public string? Name { get; set; }
}