using Farmaco.Modelos;

namespace Farmaco.Dtos;

public class VentaResponse
{
    public int IdVenta { get; }
    public string NombreProducto { get; }
    public int Cantidad { get; }
    public decimal Precio { get; }
    public string MetodoPago { get; }
    public decimal Total { get; }
    public DateTime FechaVenta { get; }

    public VentaResponse(Ventas venta)
    {
        IdVenta = venta.IdVenta;
        NombreProducto = venta.NombreProducto;
        Cantidad = venta.Cantidad;
        Precio = venta.Precio;
        MetodoPago = venta.MetodoPago;
        Total = venta.Total;
        FechaVenta = venta.FechaVenta;
    }
}