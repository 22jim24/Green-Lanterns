namespace Farmaco.Excepciones;

public class CredencialesInvalidasException : Exception
{
    public CredencialesInvalidasException() : base("Correo o clave incorrectos") { }
}