using bShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bShop.Data.Services;

public class BrandService(IDbContextFactory<ShopContext> DbFactory) : Repository<int, Brand>(DbFactory), IBrandService
{

}

public interface IBrandService : IRepository<int, Brand>
{
}
