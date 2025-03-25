

using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {


            services.AddMassTransit(x =>
            {
                x.AddConsumers(assemblies);
                x.SetInMemorySagaRepositoryProvider();
                x.AddSagaStateMachines(assemblies);
                x.AddActivities(assemblies);
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
