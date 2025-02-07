

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services, params Assembly[] assemblies)
        {

            foreach (var assembly in assemblies)
            {
                services.AddMediatR(s=>
                {
                    s.RegisterServicesFromAssembly(assembly);
                    
                });
            }
           
            return services;
        }
    }
}
