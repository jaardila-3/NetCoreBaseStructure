using System.ComponentModel.DataAnnotations;

namespace WebApp.Web.Areas.Account.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "El {0} es obligatorio")]
    [EmailAddress(ErrorMessage = "El {0} no es válido")]
    [StringLength(40, ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 10)]
    [Display(Name = "Usuario")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(30, ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string? Password { get; set; }

    [Display(Name = "¿Recordar datos?")]
    public bool RememberMe { get; set; }
}