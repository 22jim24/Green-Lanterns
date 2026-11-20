using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Farmaco.Dtos;

namespace Farmaco.Servicios;

public class ServicioToken(IConfiguration configuracion)
{
    public TokenResponse Generar(IdentityUser usuario)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, usuario.Id),
            new(ClaimTypes.Name, usuario.UserName!),
            new(ClaimTypes.Email, usuario.Email!)
        ];

        SymmetricSecurityKey clave = new(Encoding.UTF8.GetBytes(configuracion["Jwt:Clave"]!));
        SigningCredentials credenciales = new(clave, SecurityAlgorithms.HmacSha256);

        DateTime expira = DateTime.UtcNow.AddMinutes(int.Parse(configuracion["Jwt:MinutosExpiracion"]!));

        JwtSecurityToken token = new(
            issuer: configuracion["Jwt:Emisor"],
            audience: configuracion["Jwt:Audiencia"],
            claims: claims,
            expires: expira,
            signingCredentials: credenciales);

        string texto = new JwtSecurityTokenHandler().WriteToken(token);
        return new(texto, expira);
    }
}