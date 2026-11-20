using Microsoft.AspNetCore.Identity;
using Farmaco.Dtos;
using Farmaco.Excepciones;


namespace Farmaco.Servicios;

public class ServicioAutenticacion(UserManager<IdentityUser> gestorUsuarios, ServicioToken servicioToken)
{
    public async Task<UsuarioResponse> RegistrarAsync(RegistroRequest request)
    {
        IdentityUser? existente = await gestorUsuarios.FindByEmailAsync(request.Correo);
        if (existente != null)
        {
            throw new ConflictoException("Ya existe un usuario con ese correo");
        }

        IdentityUser usuario = new() { UserName = request.Correo, Email = request.Correo };
        IdentityResult resultado = await gestorUsuarios.CreateAsync(usuario, request.Clave);

        if (!resultado.Succeeded)
        {
            string errores = string.Join(" ", resultado.Errors.Select(e => e.Description));
            throw new ArgumentException(errores);
        }

        return new(usuario.Id, usuario.Email!);
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        IdentityUser? usuario = await gestorUsuarios.FindByEmailAsync(request.Correo);
        if (usuario == null)
        {
            throw new CredencialesInvalidasException();
        }

        bool claveCorrecta = await gestorUsuarios.CheckPasswordAsync(usuario, request.Clave);
        if (!claveCorrecta)
        {
            throw new CredencialesInvalidasException();
        }

        return servicioToken.Generar(usuario);
    }
}