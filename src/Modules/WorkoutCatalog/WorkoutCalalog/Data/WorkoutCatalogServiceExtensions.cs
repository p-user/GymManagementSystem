using Microsoft.Extensions.Configuration;
using Shared.Data.Interceptors;

namespace WorkoutCalalog.Data
{
    public static class WorkoutCatalogServiceExtensions
    {

        public static IServiceCollection AddWorkoutCatalogData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<AuditableEntityInterceptors>();
            services.AddScoped<DispatchDomainEventsInterceptors>();


            services.AddDbContext<WorkoutCatalogDbContext>((sp, options) =>
            {
                var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptors>();
                var dispatchInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptors>();

                options.AddInterceptors(auditableInterceptor, dispatchInterceptor);
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
