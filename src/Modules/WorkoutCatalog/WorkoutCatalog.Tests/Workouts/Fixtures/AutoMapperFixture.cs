
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace WorkoutCatalog.Tests.Workouts.Fixtures
{
    public class AutoMapperFixture
    {
        public IMapper Mapper { get; }
        public AutoMapperFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register AutoMapper profiles

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var configProvider = serviceProvider.GetRequiredService<IMapper>().ConfigurationProvider;

          
            Mapper = serviceProvider.GetRequiredService<IMapper>();
        }

    }
}
