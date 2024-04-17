using Sandbox.Contracts.Auth;
using Sandbox.Wordbook.API.Contexts;

namespace Sandbox.Wordbook.API;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        return services.AddScoped<IAuthenticatedContext, AuthenticatedContext>();
    }
}