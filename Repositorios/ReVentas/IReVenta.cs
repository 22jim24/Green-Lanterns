
using Farmaco.Modelos;

namespace Farmaco.Repositorios;

public interface IReVenta
{
    void Agregar(Ventas venta);
    void Actualizar(Ventas venta);
    Ventas? BuscarPorId(int idVenta);
    List<Ventas> ListarTodos();
    bool EliminarPorId(int idVenta);
}