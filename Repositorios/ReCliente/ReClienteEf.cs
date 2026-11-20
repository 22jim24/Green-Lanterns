using Microsoft.EntityFrameworkCore;
using Farmaco.Datos;
using Farmaco.Modelos;

namespace Farmaco.Repositorios;

public class ReClienteEf(AppDbContext contexto) : IReCliente
{
    public List<Cliente> ListarTodos()
    {
        return contexto.Clientes.ToList();
    }

    public Cliente? BuscarPorDni(string dni)
    {
        return contexto.Clientes.FirstOrDefault(c => c.Dni == dni);
    }

    public void Agregar(Cliente cliente)
    {
        contexto.Clientes.Add(cliente);
        contexto.SaveChanges();
    }

    public void Actualizar(Cliente cliente)
    {
        contexto.Clientes.Update(cliente);
        contexto.SaveChanges();
    }

    public bool EliminarPorDni(string dni)
    {
        Cliente? cliente = BuscarPorDni(dni);
        if (cliente == null)
        {
            return false;
        }
        contexto.Clientes.Remove(cliente);
        contexto.SaveChanges();
        return true;
    }

    public Cliente? BuscarConVentas(string dni)
    {
        return contexto.Clientes
            .AsNoTracking()
            .Include(c => c.Ventas)
            .FirstOrDefault(c => c.Dni == dni);
    }
}