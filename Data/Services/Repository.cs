using bShop.Data.Extensions;
using bShop.Data.Filters;
using bShop.Data.Interfaces;
using bShop.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace bShop.Data.Services;

public abstract class Repository<TKey, TEntity>(IDbContextFactory<ShopContext> DbFactory) : IRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IUniqueEntity<TKey>
{
    public virtual async Task<PageList<VM>> GetAllAsync<VM>(BaseFilter? filter = null) where VM : class
    {
        using var db = await DbFactory.CreateDbContextAsync();
        return await db.Set<TEntity>().AsNoTracking().ToPageListAsync<TEntity, VM>(filter);
    }

    public virtual async Task<TEntity?> GetAsync(TKey id)
    {
        ArgumentNullException.ThrowIfNull(id);
        using var db = await DbFactory.CreateDbContextAsync();
        return await db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using var db = await DbFactory.CreateDbContextAsync();
        await db.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using var db = await DbFactory.CreateDbContextAsync();
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity?> DeleteAsync(TKey id)
    {
        using var db = await DbFactory.CreateDbContextAsync();
        var entity = await GetAsync(id);
        if (entity is null) return null;
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }
}

public interface IRepository<in TKey, TEntity> 
    where TKey : IEquatable<TKey>
    where TEntity : class, IUniqueEntity<TKey>
{
    Task<PageList<VM>> GetAllAsync<VM>(BaseFilter? filter = null) where VM : class;
    Task<TEntity?> GetAsync(TKey id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity?> DeleteAsync(TKey id);
}
