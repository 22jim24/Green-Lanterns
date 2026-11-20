namespace Farmaco.Dtos;

public class ResultadoPaginado<T>(List<T> datos, int total, int pagina, int tamanoPagina)
{
    public List<T> Datos { get; } = datos;
    public int Total { get; } = total;
    public int Pagina { get; } = pagina;
    public int TamanoPagina { get; } = tamanoPagina;
    public int TotalPaginas { get; } = (int)Math.Ceiling(total / (double)tamanoPagina);
}