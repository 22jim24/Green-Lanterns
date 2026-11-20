using Farmaco.Dtos;
using Farmaco.Modelos;
using Farmaco.Repositorios;
using Farmaco.Excepciones;

namespace Farmaco.Servicios;

public class ServicioCliente(IReCliente repositorio, IReVenta repositorioVentas)
{
    public List<Cliente> ListarTodos()
    {
        return repositorio.ListarTodos();
    }

    public Cliente BuscarPorDni(string dni)
    {
        Cliente? cliente = repositorio.BuscarPorDni(dni) 
            ?? throw new RecursoNoEncontradoException($"No existe un cliente con DNI {dni}");
        return cliente;
    }

    public List<Cliente> ListarClientesFrecuentes()
    {
        return repositorio.ListarTodos().Where(c => c.PuntosConseguidos >= 100).ToList();
    }

    public Cliente Crear(CreateCliente request)
    {
        if (repositorio.BuscarPorDni(request.Dni) != null)
        {
            throw new ConflictoException($"Ya existe un cliente con DNI {request.Dni}");
        }

        Cliente nuevo = new(request.Dni, request.Nombre, request.Apellido, request.Correo, request.Telefono, request.PuntosConseguidos ?? 0);
        repositorio.Agregar(nuevo);
        return nuevo;
    }

    public Cliente Actualizar(string dni, UpdateCliente request)
    {
        Cliente cliente = BuscarPorDni(dni);
        cliente.ActualizarDatosPersonales(request.Nombre, request.Apellido, request.Correo, request.Telefono);
        cliente.SumarPuntos(request.PuntosConseguidos ?? 0);
        repositorio.Actualizar(cliente);
        return cliente;
    }

    public void Eliminar(string dni)
    {
        bool eliminado = repositorio.EliminarPorDni(dni);
        if (eliminado == false)
        {
            throw new RecursoNoEncontradoException($"No existe un cliente con DNI {dni}");
        }
    }

    public List<Ventas> ListarVentas(string dni)
    {
        BuscarPorDni(dni); // Lanza excepción si el cliente no existe
        
        // ReVentaEf necesita un método ListarPorCliente. Si no lo has puesto en IReVenta,
        // deberás agregarlo (ver abajo). De lo contrario, puedes hacer el Where aquí,
        // aunque lo ideal es que el repositorio lo haga.
        return repositorioVentas.ListarTodos().Where(v => v.ClienteDni == dni).ToList();
    }
}