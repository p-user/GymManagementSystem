

using Microsoft.AspNetCore.Builder;

namespace StaffManagement.Data
{
    public static  class StaffServiceExtensions
    {

        public static IServiceCollection AddStaffData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StaffDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
           
            return services;
        }


        public static IApplicationBuilder UseStaffModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StaffDbContext>();
            //dbContext.Database.Migrate();
            return app;
        }
    }
}
