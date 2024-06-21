using AutoMapper;
using bShop.Server.Data.Entities.Languages;
using bShop.Server.Data.Entities.Users;
using bShop.Shared.Models.Languages;
using bShop.Shared.Models.Users;

namespace bShop.Server;

public static class MapperProfile
{
    public static MapperConfiguration Config => _Config;
    private static readonly MapperConfiguration _Config = new(cfg =>
    {
        cfg.CreateMap<RegisterRequestVM, ShopUser>().ReverseMap();
        cfg.CreateMap<ProfileRequestVM, ShopUser>().ReverseMap();
        cfg.CreateMap<LanguageVM, Language>().ReverseMap();
    });
}
