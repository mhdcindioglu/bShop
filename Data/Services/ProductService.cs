﻿using bShop.Data.Entities;
using bShop.Data.Enums;
using bShop.Data.Extensions;
using bShop.Data.Filters;
using bShop.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using static bShop.Components.Pages.ECommerce.ProductsPage;

namespace bShop.Data.Services;

public class ProductService(IDbContextFactory<ShopContext> DbFactory, ICategoryService CategorySrv, IBrandService BrandSrv) : Repository<int, Product>(DbFactory), IProductService
{
    public async Task<PageList<ProductCardVM>> GetAllAsync(ProductsFilter filter)
    {
        using var db = await DbFactory.CreateDbContextAsync();
        var qry = db.Products.Include(x => x.Images).Include(x => x.CartItems).AsNoTracking();

        if (filter.Categories.SelectedItems().Length > 0)
            qry = qry.Where(x => filter.Categories.SelectedItems().Select(x => x.Id).Contains(x.CategoryId));

        if (filter.Brands.SelectedItems().Length > 0)
            qry = qry.Where(x => filter.Brands.SelectedItems().Select(x => x.Id).Contains(x.BrandID));

        qry = filter.Sort switch
        {
            Sort.NameAsc => qry.OrderBy(x => x.Name),
            Sort.NameDesc => qry.OrderByDescending(x => x.Name),
            Sort.DateAsc => qry.OrderBy(x => x.CreationDate),
            Sort.DateDesc => qry.OrderByDescending(x => x.CreationDate),
            _ => qry.OrderByDescending(x => x.Id),
        };

        return await qry.ToPageListAsync<Product, ProductCardVM>(filter);
    }

    public async Task<ProductsPageModel> GetProductsPageModel(ProductsPageModel model)
    {
        model.Categories = await CategorySrv.GetAllAsync<CategoryVM>();
        var category = model.Categories.FirstOrDefault(x => x.Slug == model.CategorySlug);
        if (category != null)
            model.Categories.Select([category.Id]);

        model.Brands = await BrandSrv.GetAllAsync<BrandVM>();
        var brand = model.Brands.FirstOrDefault(x => x.Slug == model.BrandSlug);
        if (brand != null)
        model.Brands.Select([brand.Id]);

        model.Products = await GetAllAsync(new ProductsFilter { Categories = model.Categories, Brands = model.Brands, CurrentPage = model.CurrentPage, Showing = model.Showing, Sort = model.Sort, });
        model.AllItems = model.Products.AllCount;
        return model;
    }
}

public interface IProductService : IRepository<int, Product>
{
    Task<PageList<ProductCardVM>> GetAllAsync(ProductsFilter filter);
    Task<ProductsPageModel> GetProductsPageModel(ProductsPageModel model);
}
