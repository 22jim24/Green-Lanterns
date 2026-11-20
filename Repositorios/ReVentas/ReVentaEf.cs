using Farmaco.Datos;
using Farmaco.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Farmaco.Repositorios;

public class ReVentaEf(AppDbContext contexto) : IReVenta
{
    public List<Ventas> ListarTodos()
    {
        return contexto.Ventas
            .AsNoTracking()
            .OrderByDescending(v => v.IdVenta)
            .ToList();
    }

    public Ventas? BuscarPorId(int idVenta)
    {
        return contexto.Ventas.FirstOrDefault(v => v.IdVenta == idVenta);
    }

    public void Agregar(Ventas venta)
    {
        contexto.Ventas.Add(venta);
        contexto.SaveChanges();
    }

    public void Actualizar(Ventas venta)
    {
        contexto.Ventas.Update(venta);
        contexto.SaveChanges();
    }

    public bool EliminarPorId(int idVenta)
    {
        Ventas? venta = BuscarPorId(idVenta);
        if (venta == null)
        {
            return false;
        }
        contexto.Ventas.Remove(venta);
        contexto.SaveChanges();
        return true;
    }
}