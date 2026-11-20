using Farmaco.Datos;
using Farmaco.Modelos;

namespace Farmaco.Repositorios;

public class ReProveedorEf(AppDbContext contexto) : IReProveedor
{
    public List<Proovedor> ListarTodos()
    {
        return contexto.Proveedores.ToList();
    }

    public Proovedor? BuscarPorNombre(string nombreProveedor)
    {
        return contexto.Proveedores.FirstOrDefault(p => p.NombreProveedor == nombreProveedor);
    }

    public void Agregar(Proovedor proveedor)
    {
        contexto.Proveedores.Add(proveedor);
        contexto.SaveChanges();
    }

    public void Actualizar(Proovedor proveedor)
    {
        contexto.Proveedores.Update(proveedor);
        contexto.SaveChanges();
    }

    public bool EliminarPorNombre(string nombreProveedor)
    {
        Proovedor? proveedor = BuscarPorNombre(nombreProveedor);
        if (proveedor == null)
        {
            return false;
        }
        contexto.Proveedores.Remove(proveedor);
        contexto.SaveChanges();
        return true;
    }
}