using bShop.Localization;

namespace bShop.Client.Exceptions;

public class ReCAPTCHASecretKeyIsNullException : Exception
{
    public ReCAPTCHASecretKeyIsNullException() : base(GetDefaultMessage()) { }
    public ReCAPTCHASecretKeyIsNullException(string message) : base(string.IsNullOrEmpty(message) ? GetDefaultMessage() : message) { }
    public ReCAPTCHASecretKeyIsNullException(string message, Exception innerException) : base(string.IsNullOrEmpty(message) ? GetDefaultMessage() : message, innerException) { }
    private static string GetDefaultMessage() => Resources.ExceptionReCAPTCHASecretKeyIsNull;
    public override string Message => string.IsNullOrEmpty(base.Message) ? GetDefaultMessage() : base.Message;
}