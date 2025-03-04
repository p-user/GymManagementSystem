﻿

using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;
using System.Reflection;

namespace WorkoutCalalog.Data
{
    public static class WorkoutCatalogServiceExtensions
    {

        public static IServiceCollection AddWorkoutCatalogData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ISaveChangesInterceptor, SaveChangesInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();
            services.AddDbContext<WorkoutCatalogDbContext>((sp,options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
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
