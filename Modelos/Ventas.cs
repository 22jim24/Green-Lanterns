using System.Text.Json.Serialization;
namespace Farmaco.Modelos;

public class Ventas
{
    public int IdVenta { get; set; }
    public string NombreCliente { get; private set; } 
    public string NombreProducto { get; private set; } 
    public int Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public string MetodoPago { get; private set; } ="";
    public decimal Total { get; private set; }
    public DateTime FechaVenta { get; } = DateTime.Now;

    //CLAVE FORÁNEA 
    public string ClienteDni { get; private set; }

    //Easter egg de navegante
    [JsonIgnore]
    public Cliente? Cliente { get; private set; }

    public Ventas(string clienteDni, string nombreCliente, string nombreProducto, int cantidad, decimal precio, string metodoPago)
    {
        ClienteDni = clienteDni;
        NombreCliente = nombreCliente;
        NombreProducto = nombreProducto;
        CambiarCantidad(cantidad);
        CambiarPrecio(precio);
        CambiarMetodoPago(metodoPago);
        RecalcularTotal(); // El total se calcula automáticamente aquí adentro
    }

    public void CambiarNombreCliente(string nombre)
    {
        NombreCliente = nombre;
    }

    public void CambiarNombreProducto(string nombre)
    {
        NombreProducto = nombre;
    }
    public void CambiarMetodoPago(string metodo)
    {
        if (metodo != "EFECTIVO" && metodo != "TARJETA" && metodo != "YAPE_PLIN" && metodo != "TRANSFERENCIA")
        {
            throw new ArgumentException("Método de pago no válido");
        }
        MetodoPago = metodo;
    }
    public void CambiarCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor que cero");
        }
        Cantidad = nuevaCantidad;
    }
    public void CambiarPrecio(decimal nuevoPrecio)
    {
        if (nuevoPrecio < 0)
        {
            throw new ArgumentException("El precio no puede ser negativo");
        }
        Precio = nuevoPrecio;
    }

    public void RecalcularTotal()
    {
        Total = Cantidad * Precio;
    }

}