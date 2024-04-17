using Gateway.Services.Abstractions;

namespace Gateway.Services;

public static class ServicesExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        return serviceCollection;
    }
}