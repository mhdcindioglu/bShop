using AutoMapper.QueryableExtensions;
using bShop.Server.Data;
using bShop.Server.Extensions;
using bShop.Shared.Models;
using bShop.Shared.Models.Languages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bShop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController(IDbContextFactory<ShopDB> DbContextFactory) : ControllerBase
{

    [HttpPost("Languages")]
    public async Task<ApiResult<LanguageVM[]>> Languages()
    {
        var response = new ApiResult<LanguageVM[]>();

        try
        {
            using var db = await DbContextFactory.CreateDbContextAsync();
            response.Results = await db.Languages.NotDeletedAndActive().ProjectTo<LanguageVM>().ToArrayAsync();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerMessage());
            response.AddError(ex.InnerMessage());
        }

        return response;
    }
}