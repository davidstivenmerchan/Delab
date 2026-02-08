using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delab.Shared.Entities;

public class User : IdentityUser
{
    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Nombres")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Apellidos")]
    public string LastName { get; set; } = null!;

    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    public string? FullName { get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Puesto de Trabajo")]
    public string JobPosition { get; set; } = null!;

    [Display(Name = "Origen")]
    public string? UserFrom { get; set; }

    [Display(Name = "Foto")]
    public string? PhotoUser { get; set; }

    [Display(Name = "Activo")]
    public bool Active { get; set; }

    [NotMapped]
    public string? Pass { get; set; }

    public int? CorporationId { get; set; }

    //Relaciones
    public Corporation? Corporation { get; set; }

    public ICollection<UserRoleDetails>? UserRoleDetails { get; set; }
}
