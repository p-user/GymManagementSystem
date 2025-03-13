
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
            services.AddScoped<AuditableEntityInterceptors>();
            services.AddScoped<DispatchDomainEventsInterceptors>();


            services.AddDbContext<MembershipDbContext>((sp,options) =>
            {
                var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptors>();
                var dispatchInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptors>();

                options.AddInterceptors(auditableInterceptor, dispatchInterceptor);
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
