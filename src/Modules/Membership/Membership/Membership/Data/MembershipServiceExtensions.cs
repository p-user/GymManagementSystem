
using Microsoft.AspNetCore.Builder;

namespace Membership.Data
{
    public static class MembershipServiceExtensions
    {

        public static IServiceCollection AddMembershipData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MembershipDbContext>(options =>
            {
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
