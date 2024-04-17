using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Wordbook.Domain.Abstractions;
using Sandbox.Wordbook.Persistence.Interceptors;
using Sandbox.Wordbook.Persistence.Repositories;

namespace Sandbox.Wordbook.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<IUnitOfWork, WordbookContext>(
            opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("WordbookDatabase"));
                opt.AddInterceptors(new AuditInterceptor());
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
            });

        serviceCollection.AddScoped<DatabaseMigrator>();

        serviceCollection.AddScoped<IWordbookUserRepository, WordbookUserRepository>();
        serviceCollection.AddScoped<ITranslationRepository, TranslationRepository>();

        return serviceCollection;
    }
}