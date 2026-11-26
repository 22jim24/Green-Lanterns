using Farmaco.Dtos;
using Farmaco.Datos;
using Farmaco.Modelos;
using Farmaco.Repositorios;
using Farmaco.Excepciones;
using Microsoft.EntityFrameworkCore.Storage;

namespace Farmaco.Servicios;

public class ServicioVentas(IReVenta repositorio, AppDbContext contexto)
{
    public List<Ventas> ListarTodos()
    {
        return repositorio.ListarTodos();
    }

    public Ventas BuscarPorId(int idVenta)
    {
        Ventas? venta = repositorio.BuscarPorId(idVenta) 
            ?? throw new RecursoNoEncontradoException($"No existe una venta con el ID {idVenta}");
        return venta;
    }

    public Ventas Crear(CreateVenta request)
    {
        using (IDbContextTransaction transaccion = contexto.Database.BeginTransaction())
        {
            Ventas nueva = new(
                request.ClienteDni,
                request.NombreCliente,
                request.NombreProducto,
                request.Cantidad ?? 0,
                request.Precio ?? 0,
                request.MetodoPago
            );

            repositorio.Agregar(nueva);

            // Aquí iría cualquier segunda escritura (ej. descontar stock del producto)

            //throw new Exception("Falla simulada");

            transaccion.Commit();
            return nueva;
        }
    }

    public Ventas Actualizar(int idVenta, UpdateVenta request)
    {
        Ventas venta = BuscarPorId(idVenta);
        venta.CambiarNombreCliente(request.NombreCliente);
        venta.CambiarNombreProducto(request.NombreProducto);
        venta.CambiarCantidad(request.Cantidad ?? 0);
        venta.CambiarPrecio(request.Precio ?? 0);
        venta.CambiarMetodoPago(request.MetodoPago);
        venta.RecalcularTotal();

        repositorio.Actualizar(venta);
        return venta;
    }

    public void Eliminar(int idVenta)
    {
        bool eliminado = repositorio.EliminarPorId(idVenta);
        if (!eliminado)
        {
            throw new RecursoNoEncontradoException($"No existe una venta con el ID {idVenta}");
        }
    }
}