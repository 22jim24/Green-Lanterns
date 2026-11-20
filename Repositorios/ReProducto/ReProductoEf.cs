using Microsoft.EntityFrameworkCore;

using Farmaco.Datos;
using Farmaco.Modelos;
using Farmaco.Dtos;


namespace Farmaco.Repositorios;

public class ReProductoEf(AppDbContext contexto) : IReProducto
{
    public List<Producto> ListarTodos()
    {
        return contexto.Productos.ToList();
    }

    public Producto? BuscarPorNombre(string nombre)
    {
        return contexto.Productos.FirstOrDefault(p => p.Nombre == nombre);
    }

    public void Agregar(Producto producto)
    {
        contexto.Productos.Add(producto);
        contexto.SaveChanges();
    }

    public void Actualizar(Producto producto)
    {
        contexto.Productos.Update(producto);
        contexto.SaveChanges();
    }

    public bool EliminarPorNombre(string nombre)
    {
        Producto? producto = BuscarPorNombre(nombre);
        if (producto == null)
        {
            return false;
        }
        contexto.Productos.Remove(producto);
        contexto.SaveChanges();
        return true;
    }

    public ResultadoPaginado<Producto> Buscar(FiltroProducto filtro)
    {
        IQueryable<Producto> consulta = contexto.Productos.AsNoTracking();

        // 1. Filtros opcionales
        if (!string.IsNullOrWhiteSpace(filtro.Nombre))
        {
            consulta = consulta.Where(p => EF.Functions.Like(p.Nombre, $"%{filtro.Nombre}%"));
        }

        if (filtro.PrecioMinimo != null)
        {
            consulta = consulta.Where(p => p.Precio >= filtro.PrecioMinimo);
        }

        // 2. Ordenamiento
        if (filtro.Orden == "precio")
        {
            consulta = consulta.OrderByDescending(p => p.Precio);
        }
        else
        {
            consulta = consulta.OrderBy(p => p.Nombre);
        }

        // 3. Total y paginación
        int total = consulta.Count();
        List<Producto> datos = consulta
            .Skip((filtro.Pagina - 1) * filtro.Tamano)
            .Take(filtro.Tamano)
            .ToList();

        return new ResultadoPaginado<Producto>(datos, total, filtro.Pagina, filtro.Tamano);
    }
}