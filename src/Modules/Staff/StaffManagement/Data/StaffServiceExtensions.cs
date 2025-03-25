using Shared.Data.Interceptors;

namespace StaffManagement.Data
{
    public static class StaffServiceExtensions
    {

        public static IServiceCollection AddStaffData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<AuditableEntityInterceptors>();
            services.AddScoped<DispatchDomainEventsInterceptors>();


            services.AddDbContext<StaffDbContext>((sp, options) =>
            {
                var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptors>();
                var dispatchInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptors>();

                options.AddInterceptors(auditableInterceptor, dispatchInterceptor);
                options.UseSqlServer(connectionString);
            });

            return services;
        }


        public static IApplicationBuilder UseStaffModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StaffDbContext>();
            dbContext.Database.Migrate();
            return app;
        }
    }
}
