
using AutoMapper;
using bShop.Data.Entities;
using bShop.Data.ViewModels;

namespace bShop.Data;

public static class MapperProfile
{
    public static MapperConfiguration Config => _Config;
    private static readonly MapperConfiguration _Config = new(cfg =>
    {
        cfg.CreateMap<Category, CategoryVM>();
        cfg.CreateMap<Brand, BrandVM>();
        cfg.CreateMap<Product, ProductCardVM>();
        cfg.CreateMap<Product, ProductVM>();
        cfg.CreateMap<Vendor, VendorVM>();
        cfg.CreateMap<Collection, CollectionVM>();
        cfg.CreateMap<Label, LabelVM>();
    });
}
