using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Wordbook.Application.Events.Handlers;
using Sandbox.Wordbook.Application.Mappers;
using Sandbox.Wordbook.Application.Translation.Commands.CreateTranslation;

namespace Sandbox.Wordbook.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        serviceCollection.AddValidatorsFromAssemblyContaining(typeof(CreateTranslationValidator));

        serviceCollection.AddAutoMapper(typeof(TranslationProfile).Assembly);

        serviceCollection.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(config["MessageBroker:Host"]!), x =>
                {
                    x.Username(config["MessageBroker:Username"]!);
                    x.Password(config["MessageBroker:Password"]!);
                });

                cfg.ConfigureEndpoints(context);
            });

            busConfig.AddConsumer<UserCreatedEventConsumer>();
        });

        return serviceCollection;
    }
}