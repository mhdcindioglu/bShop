using bShop.Data.Entities;
using bShop.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.Services;

public class NewsLetterService(IDbContextFactory<ShopContext> DbFactory) : INewsLetterService
{
    public async Task<Response<NewsLetter>> AddToList(NewsLetter model)
    {
        var response = new Response<NewsLetter>();

        try
        {
            using var db = await DbFactory.CreateDbContextAsync();
            if (await db.NewsLetters.AnyAsync(x => x.Email == model.Email))
                return response;
            else
            {
                await db.NewsLetters.AddAsync(model);
                await db.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            response.AddError(ex.Message);
        }

        return response;
    }
}

public interface INewsLetterService
{
    Task<Response<NewsLetter>> AddToList(NewsLetter model);
}
