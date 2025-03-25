using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviours;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services, params Assembly[] assemblies)
        {

            foreach (var assembly in assemblies)
            {
                services.AddMediatR(s =>
                {
                    s.RegisterServicesFromAssembly(assembly);
                    s.AddOpenBehavior(typeof(Logging<,>));

                });

            }



            return services;
        }
    }
}
