
namespace bShop.Server.Exceptions;
public class ConnectionStringNotFoundException(string cs) : Exception(string.Format(Resources.ExceptionConnectionStringNotFound, cs)) { }