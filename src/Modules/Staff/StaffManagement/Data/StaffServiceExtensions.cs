

using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data.Interceptors;

namespace StaffManagement.Data
{
    public static  class StaffServiceExtensions
    {

        public static IServiceCollection AddStaffData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptors>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

            services.AddDbContext<StaffDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
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
