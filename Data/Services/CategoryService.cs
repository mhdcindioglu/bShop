using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.Services;

public class CategoryService(IDbContextFactory<ShopContext> DbFactory) : Repository<int, Category>(DbFactory), ICategoryService
{

}

public interface ICategoryService : IRepository<int, Category>
{
}
