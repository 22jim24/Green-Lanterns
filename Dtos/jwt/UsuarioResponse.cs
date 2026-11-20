namespace Farmaco.Dtos;

public class UsuarioResponse(string id, string correo)
{
    public string Id { get; } = id;
    public string Correo { get; } = correo;
}