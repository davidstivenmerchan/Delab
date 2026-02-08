using System.ComponentModel.DataAnnotations;

namespace Delab.Shared.ResponsesSec;

public class LoginDTO
{
    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    public string Email { get; set; } = null!;


    [Display(Name = "Clave")]
    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres")]
    public string Password { get; set; } = null!;
}
