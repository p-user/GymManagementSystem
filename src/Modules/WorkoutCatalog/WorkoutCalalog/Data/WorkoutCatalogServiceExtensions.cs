

using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WorkoutCalalog.Data
{
    public static class WorkoutCatalogServiceExtensions
    {

        public static IServiceCollection AddWorkoutCatalogData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<WorkoutCatalogDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
           


            return services;
        }


        public static IApplicationBuilder UseWorkoutCatalogModule(this IApplicationBuilder app)
        {
           
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WorkoutCatalogDbContext>();
            //dbContext.Database.Migrate();
            return app;
        }
    }
}
