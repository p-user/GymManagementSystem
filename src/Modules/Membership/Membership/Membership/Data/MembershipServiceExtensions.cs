
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data.Interceptors;

namespace Membership.Data
{
    public static class MembershipServiceExtensions
    {

        public static IServiceCollection AddMembershipData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptors>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();


            services.AddDbContext<MembershipDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseMembershipModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
            dbContext.Database.Migrate();
            return app;
        }
    }
}
