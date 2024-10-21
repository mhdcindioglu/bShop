using AutoMapper.QueryableExtensions;
using bShop.Data.Filters;
using bShop.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.Extensions;

public static class QueryableExtensions
{
    public static async Task<PageList<VM>> ToPageListAsync<T, VM>(this IQueryable<T> qry, BaseFilter? filter) 
        where T : class 
        where VM : class
    {
        PageList<VM> result = [];

        result.AllCount = await qry.CountAsync();

        if (filter == null)
            result.AddRange(await qry.ProjectTo<VM>().ToListAsync());
        else
        {
            result.AddRange(await qry.ProjectTo<VM>()
                .Skip((int)filter.Showing * (filter.CurrentPage - 1))
                .Take((int)filter.Showing)
                .ToListAsync());
        }

        result.AllCount = await qry.CountAsync();
        return result;
    }

    public static IQueryable<T> ProjectTo<T>(this IQueryable<object> objects) =>
            objects.ProjectTo<T>(MapperProfile.Config.CreateMapper().ConfigurationProvider);
}
