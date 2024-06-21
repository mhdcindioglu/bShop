namespace bShop.Shared.Extensions;
public static class DateTimeExtensions
{
    public static string ToTimeStamp(this DateTime? date) =>
        date == null ? "" : new DateTimeOffset(date.Value).ToUnixTimeMilliseconds().ToString();
    public static DateTime? FromTimeStamp(this long? timeStamp) =>
       timeStamp == null ? null : DateTimeOffset.FromUnixTimeMilliseconds(timeStamp.Value).DateTime;
}
