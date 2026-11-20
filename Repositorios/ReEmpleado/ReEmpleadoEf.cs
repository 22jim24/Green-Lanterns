using Farmaco.Datos;
using Farmaco.Modelos;

namespace Farmaco.Repositorios;

public class ReEmpleadoEf(AppDbContext contexto) : IReEmpleado
{
    public List<Empleado> ListarTodos()
    {
        return contexto.Empleados.ToList();
    }

    public Empleado? BuscarPorDni(string dni)
    {
        return contexto.Empleados.FirstOrDefault(e => e.Dni == dni);
    }

    public void Agregar(Empleado empleado)
    {
        contexto.Empleados.Add(empleado);
        contexto.SaveChanges();
    }

    public void Actualizar(Empleado empleado)
    {
        contexto.Empleados.Update(empleado);
        contexto.SaveChanges();
    }

    public bool EliminarPorDni(string dni)
    {
        Empleado? empleado = BuscarPorDni(dni);
        if (empleado == null)
        {
            return false;
        }
        contexto.Empleados.Remove(empleado);
        contexto.SaveChanges();
        return true;
    }
}