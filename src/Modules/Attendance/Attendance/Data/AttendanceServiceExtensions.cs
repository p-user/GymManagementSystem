

namespace Attendance.Data
{
    public static  class AttendanceServiceExtensions
    {
        public static IServiceCollection AddAttendanceData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<AuditableEntityInterceptors>();
            services.AddScoped<DispatchDomainEventsInterceptors>();


            services.AddDbContext<AttendanceDbContext>((sp, options) =>
            {
                var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptors>();
                var dispatchInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptors>();

                options.AddInterceptors(auditableInterceptor, dispatchInterceptor);
                options.UseSqlServer(connectionString);
            });

            return services;
        }


        public static IApplicationBuilder UseAttendanceModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AttendanceDbContext>();
            dbContext.Database.Migrate();
            return app;
        }
    }
}
