using Microsoft.AspNetCore.Mvc;
using Farmaco.Dtos;
using Farmaco.Modelos;
using Farmaco.Servicios;
using Microsoft.AspNetCore.Authorization;


namespace Farmaco.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize] // ← todo cerrado
public class VentasController(ServicioVentas servicio) : ControllerBase
{
    [AllowAnonymous] // ← abierto

    [HttpGet]
    public ActionResult<List<Ventas>> ListarTodos()
    {
        return Ok(servicio.ListarTodos());
    }

    [HttpGet("{idVenta}")]
    public ActionResult<Ventas> BuscarPorId(int idVenta)
    {
        return Ok(servicio.BuscarPorId(idVenta));
    }

    [HttpPost]
    public ActionResult<Ventas> Crear(CreateVenta request)
    {
        Ventas nueva = servicio.Crear(request);
        return Created($"/api/ventas/{nueva.IdVenta}", nueva);
    }

    [HttpPut("{idVenta}")]
    public ActionResult<Ventas> Actualizar(int idVenta, UpdateVenta request)
    {
        Ventas actualizado = servicio.Actualizar(idVenta, request);
        return Ok(actualizado);
    }

    [HttpDelete("{idVenta}")]
    public IActionResult Eliminar(int idVenta)
    {
        servicio.Eliminar(idVenta);
        return Ok("Venta eliminada correctamente.");
    }
}