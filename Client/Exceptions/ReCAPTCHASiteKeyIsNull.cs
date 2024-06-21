using bShop.Localization;

namespace bShop.Client.Exceptions;

public class ReCAPTCHASiteKeyIsNullException : Exception
{
    public ReCAPTCHASiteKeyIsNullException() : base(GetDefaultMessage()) { }
    public ReCAPTCHASiteKeyIsNullException(string message) : base(string.IsNullOrEmpty(message) ? GetDefaultMessage() : message) { }
    public ReCAPTCHASiteKeyIsNullException(string message, Exception innerException) : base(string.IsNullOrEmpty(message) ? GetDefaultMessage() : message, innerException) { }
    private static string GetDefaultMessage() => Resources.ExceptionReCAPTCHASiteKeyIsNull;
    public override string Message => string.IsNullOrEmpty(base.Message) ? GetDefaultMessage() : base.Message;
}