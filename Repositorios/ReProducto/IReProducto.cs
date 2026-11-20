using Farmaco.Modelos;
using Farmaco.Dtos;

namespace Farmaco.Repositorios;

public interface IReProducto
{
    void Agregar(Producto producto);
    Producto? BuscarPorNombre(string nombre);
    List<Producto> ListarTodos();
    bool EliminarPorNombre(string nombre);
    void Actualizar(Producto producto);
    ResultadoPaginado<Producto> Buscar(FiltroProducto filtro);
}