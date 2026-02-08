using Delab.Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Delab.Shared.Entities;

public class Manager
{
    [Key]
    public int ManagerId { get; set; }

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Nombres")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Apellido")]
    public string LastName { get; set; } = null!;

    [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(15, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "RUC o DNI")]
    public string? Nro_Document { get; set; }

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(15, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Teléfono")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(15, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Dirección")]
    public string? Address { get; set; }

    //Correo o nombre de usuario
    [DataType(DataType.EmailAddress)]
    [MaxLength(256, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Email")]
    public string? UserName { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una corporación")]
    [Display(Name = "Corporación")]
    public int CorporationId { get; set; }

    [Required(ErrorMessage = "El campo {0} es Obligatorio")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor de {1} carácteres")]
    [Display(Name = "Puesto de Trabajo")]
    public string? Job { get; set; }

    [Display(Name = "Tipo de Usuario")]
    public UserType UserType { get; set; }

    [Display(Name = "Foto")]
    public string? Photo { get; set; }

    [Display(Name = "Activo")]
    public bool Active { get; set; }

    public string ImageFullPath => Photo == string.Empty || Photo == null
    ? $"https://localhost:7193/Images/NoImage.png"
    : $"https://localhost:7193/Images/ImgManager/{Photo}";

    [NotMapped]
    public string? ImgBase64 { get; set; }

    //Relaciones
    public Corporation? Corporation { get; set; }
}

