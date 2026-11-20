using System.ComponentModel.DataAnnotations;

namespace Farmaco.Dtos;

public class RegistroRequest
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La clave es obligatoria")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "La clave debe tener entre 6 y 50 caracteres")]
    public string Clave { get; set; } = "";
}