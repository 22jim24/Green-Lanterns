using Microsoft.EntityFrameworkCore.Storage;
using Farmaco.Dtos;
using Farmaco.Modelos;
using Farmaco.Repositorios;
using Farmaco.Excepciones;
namespace Farmaco.Servicios;

public class ServicioProductos(IReProducto repositorio)
{
    public List<Producto> ListarTodos()
    {
        return repositorio.ListarTodos();
    }

    public Producto BuscarPorNombre(string nombre)
    {
        Producto? producto = repositorio.BuscarPorNombre(nombre) 
            ?? throw new RecursoNoEncontradoException($"No existe un producto con nombre {nombre}");
        return producto;
    }


    public Producto Crear(CreateProducto request)
    {
        if (repositorio.BuscarPorNombre(request.Nombre) != null)
        {
            throw new ConflictoException($"Ya existe un producto con nombre {request.Nombre}");
        }

        Producto nuevo = new(request.Nombre, request.Precio ?? 0, request.Stock ?? 0);
        repositorio.Agregar(nuevo);
        return nuevo;
    }

    public Producto Actualizar(string nombre, UpdateProducto request)
    {
        Producto producto = BuscarPorNombre(nombre);
        producto.ActualizarNombre(request.Nombre);
        producto.ActualizarPrecio(request.Precio ?? 0);
        producto.ActualizarStock(request.Stock ?? 0);
        repositorio.Actualizar(producto);
        return producto;
    }

    public void Eliminar(string nombre)
    {
        bool eliminado = repositorio.EliminarPorNombre(nombre);
        if (eliminado == false)
        {
            throw new RecursoNoEncontradoException($"No existe un producto con nombre {nombre}");
        }
    }

    public ResultadoPaginado<Producto> Buscar(FiltroProducto filtro)
    {
        return repositorio.Buscar(filtro);
    }
}