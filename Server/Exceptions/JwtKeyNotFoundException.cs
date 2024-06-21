namespace bShop.Server.Exceptions;
public class JwtKeyNotFoundException : Exception
{
    public JwtKeyNotFoundException() : base(GetDefaultMessage()) { }
    public JwtKeyNotFoundException(string message) : base(string.IsNullOrEmpty(message) ? GetDefaultMessage() : message) { }
    public JwtKeyNotFoundException(string message, Exception innerException) : base(string.IsNullOrEmpty(message) ? GetDefaultMessage() : message, innerException) { }
    private static string GetDefaultMessage() => Resources.ExceptionJwtKeyNotFound;
    public override string Message => string.IsNullOrEmpty(base.Message) ? GetDefaultMessage() : base.Message;
}