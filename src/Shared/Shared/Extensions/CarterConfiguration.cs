
using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class CarterConfiguration
    {

        public static IServiceCollection AddCarter(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: d =>
            {
                foreach (var item in assemblies)
                {
                    var module = item.GetTypes().Where(s => s.IsAssignableTo(typeof(ICarterModule))).ToArray();
                    d.WithModules(module);


                }
            });

            return services;
        }
    }
    
}
