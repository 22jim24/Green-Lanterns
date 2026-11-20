using System.ComponentModel.DataAnnotations;

namespace Farmaco.Dtos;

public class FiltroProducto
{
    public string? Nombre { get; set; }

    [Range(0, 100000, ErrorMessage = "El precio mínimo debe ser 0 o mayor")]
    public decimal? PrecioMinimo { get; set; }

    [RegularExpression("^(nombre|precio)$", ErrorMessage = "El orden debe ser 'nombre' o 'precio'")]
    public string? Orden { get; set; }

    [Range(1, 1000, ErrorMessage = "La página debe ser 1 o mayor")]
    public int Pagina { get; set; } = 1;

    [Range(1, 50, ErrorMessage = "El tamaño de página debe estar entre 1 y 50")]
    public int Tamano { get; set; } = 10;
}