using MassTransit;

namespace Gateway.MessageBroker;

public static class MessageBrokerSetup
{
    public static IServiceCollection AddBus(this IServiceCollection serviceCollection, IConfiguration config)
    {
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
        });

        return serviceCollection;
    }
}