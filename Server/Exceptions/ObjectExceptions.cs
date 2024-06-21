namespace bShop.Server.Exceptions;

public static class ObjectExceptions
{
    public static AutoMapper.IMapper Mapper => MapperProfile.Config.CreateMapper();
    public static object Map(this object source, object destination) => Mapper.Map(source, destination);
    public static T Map<T>(this object source) => Mapper.Map<T>(source);
}
