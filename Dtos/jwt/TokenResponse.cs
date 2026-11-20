namespace Farmaco.Dtos;

public class TokenResponse(string token, DateTime expira)
{
    public string Token { get; } = token;
    public DateTime Expira { get; } = expira;
}