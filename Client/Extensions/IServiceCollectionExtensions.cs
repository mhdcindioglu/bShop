using bShop.Client.Handlers;
using Refit;

namespace bShop.Client.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppRefitClient<T>(this IServiceCollection services, Uri serverUrl) where T : class
    {
        var url = $"{serverUrl}Api/{typeof(T).Name.TrimStart('I').Replace("Client", "")}";
        services
            .AddRefitClient<T>(AppRefitSettings)
            .ConfigureHttpClient(sp => sp.BaseAddress = new Uri(url))
            .AddHttpMessageHandler<ShopHttpMessageHandler>();
        return services;
    }

    private static RefitSettings AppRefitSettings(IServiceProvider provider) =>
        new() { /*AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult("")*/ };
}
