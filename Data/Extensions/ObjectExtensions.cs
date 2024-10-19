using AutoMapper;
using System.Text.Json;

namespace bShop.Data.Extensions;

public static class ObjectExtensions
{
    public static T? DeepClone<T>(this T obj) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj));
    public static T2? DeepClone<T1,T2>(this T1 obj) => JsonSerializer.Deserialize<T2>(JsonSerializer.Serialize(obj));
}
