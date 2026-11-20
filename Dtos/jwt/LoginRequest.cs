using System.ComponentModel.DataAnnotations;

namespace Farmaco.Dtos;

public class LoginRequest
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La clave es obligatoria")]
    public string Clave { get; set; } = "";
}