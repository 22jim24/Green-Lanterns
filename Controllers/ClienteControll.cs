using Microsoft.AspNetCore.Mvc;
using Farmaco.Dtos;
using Farmaco.Modelos;
using Farmaco.Servicios;

namespace Farmaco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController(ServicioCliente servicio,ServicioVentas servicioVentas) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Cliente>> ListarTodos()
    {
        return Ok(servicio.ListarTodos());
    }

    [HttpGet("{dni}")]
    public ActionResult<Cliente> BuscarPorDni(string dni)
    {
        Cliente? cliente = servicio.BuscarPorDni(dni);
        if (cliente == null)
        {
            return NotFound($"No existe un cliente con DNI {dni}");
        }
        return Ok(cliente);
    }

    [HttpGet("frecuentes")]
    public ActionResult<List<Cliente>> ListarClientesFrecuentes()
    {
        return Ok(servicio.ListarClientesFrecuentes());
    }
    
    [HttpPost]
    public ActionResult<Cliente> Crear(CreateCliente request)
    {
        Cliente nuevo = servicio.Crear(request);
        return Created($"/api/cliente/{nuevo.Dni}", nuevo);
    }

    [HttpPut("{dni}")]
    public ActionResult<Cliente> Actualizar(string dni, UpdateCliente request)
    {
        Cliente? actualizado = servicio.Actualizar(dni, request);
        if (actualizado == null)
        {
            return NotFound($"No existe un cliente con DNI {dni}");
        }

        return Ok(actualizado);
    }

    [HttpDelete("{dni}")]
    public IActionResult Eliminar(string dni)
    {
        servicio.Eliminar(dni);
        return Ok("Cliente eliminado correctamente.");
    }

    [HttpGet("{dni}/ventas")]
    public ActionResult<List<Ventas>> ListarVentas(string dni)
    {
        return Ok(servicio.ListarVentas(dni));
    }

    [HttpPost("{dni}/ventas")]
    public ActionResult<Ventas> RegistrarVenta(string dni, CreateVenta request)
    {
        // 1. Forzamos a que el DNI del JSON sea el mismo DNI de la URL
        request.ClienteDni = dni;
        
        // 2. Llamamos al método Crear que ya tienes hecho en ServicioVentas
        Ventas nueva = servicioVentas.Crear(request);
        
        return Created($"/api/cliente/{dni}/ventas", nueva);
    }
}