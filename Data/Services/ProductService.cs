using bShop.Data.Entities;
using bShop.Data.Enums;
using bShop.Data.Extensions;
using bShop.Data.Filters;
using bShop.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using static bShop.Components.Pages.ECommerce.ProductsPage;

namespace bShop.Data.Services;

public class ProductService(ICategoryService CategorySrv, IBrandService BrandSrv) : Repository<int, Product>(DbFactory), IProductService
{
    public static required IDbContextFactory<ShopContext> DbFactory;

    public async Task<PageList<ProductCardVM>> GetAllAsync(ProductsFilter filter)
    {
        using var db = await DbFactory.CreateDbContextAsync();
        var qry = db.Products
            .Where(x => filter.Categories.SelectedItems().Length == 0 || filter.Categories.SelectedItems().Select(x => x.Id).Contains(x.CategoryId));

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
        var selectedCategories = model.Categories.SelectedItems();
        model.Categories = await CategorySrv.GetAllAsync<CategoryVM>();
        model.Categories.Select(selectedCategories.Select(x => x.Id).ToArray());

        var selectedBrands = model.Brands.SelectedItems();
        model.Brands = await BrandSrv.GetAllAsync<BrandVM>();
        model.Brands.Select(selectedBrands.Select(x => x.Id).ToArray());

        model.Products = await GetAllAsync(new ProductsFilter { Categories = model.Categories, Brands = model.Brands, CurrentPage = model.CurrentPage, Showing = model.Showing, Sort = model.Sort, });
        return model;
    }
}

public interface IProductService : IRepository<int, Product>
{
    Task<PageList<ProductCardVM>> GetAllAsync(ProductsFilter filter);
    Task<ProductsPageModel> GetProductsPageModel(ProductsPageModel model);
}
