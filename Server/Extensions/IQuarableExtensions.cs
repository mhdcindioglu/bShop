using AutoMapper.QueryableExtensions;
using bShop.Shared.Interfaces;
using System.Linq.Expressions;

namespace bShop.Server.Extensions;

public static class IQuarableExtensions
{
    public static IQueryable<T> ProjectTo<T>(this IQueryable<object> objects) =>
        objects.ProjectTo<T>(MapperProfile.Config.CreateMapper().ConfigurationProvider);

    public static IQueryable<T> NotDeleted<T>(this IQueryable<T> qry) where T : IIsDeletable =>
     qry.Where(x => !x.IsDeleted);

    public static IQueryable<T> NotDeletedAndActive<T>(this IQueryable<T> qry) where T : IIsDeletableActivable =>
        qry.Where(x => !x.IsDeleted && x.IsActive);

    public static IQueryable<T> IsActive<T>(this IQueryable<T> qry) where T : IIsActivable =>
        qry.Where(x => x.IsActive);

    public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "OrderBy");

    public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "OrderByDescending");

    public static IOrderedQueryable<T> ThenByColumn<T>(this IOrderedQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "ThenBy");

    public static IOrderedQueryable<T> ThenByColumnDescending<T>(this IOrderedQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "ThenByDescending");

    private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var member = columnPath.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);
        var keySelector = Expression.Lambda(member, parameter);
        var methodCall = Expression.Call(typeof(Queryable), method, new[]
                { parameter.Type, member.Type },
            source.Expression, Expression.Quote(keySelector));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }
}
