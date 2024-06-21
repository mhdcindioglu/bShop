using Microsoft.Data.SqlClient;

namespace bShop.Shared.Extensions;
public static class ExceptionExtensions
{
    public static string InnerMessage(this Exception ex)
    {
        if (ex is SqlException exception)
        {
            if (exception.ErrorCode == -2146232060)
                return $"Need migration because there are some missing properties in Database:\r\n{GetInnerMessage(exception)}";
            else
                return GetInnerMessage(exception);
        }
        else
            return GetInnerMessage(ex);
    }

    private static string GetInnerMessage(Exception ex) =>
        ex.InnerException?.Message ?? ex.Message + "\r\n\r\n" + ex.StackTrace;
}
