
using Authentication.Data.Seed;
using Authentication.Models;
using Authentication.Services;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Authentication.Data
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddAuthenticationData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IDiscoveryService, DiscoveryService>();

            services.AddIdentity<Models.User, Models.Role>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.IssuerUri = configuration["IdentityServer"];
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddProfileService<ProfileService>()  // Custom claims enrichment
            .AddInMemoryClients(Clients.GetClients())
            .AddInMemoryApiScopes(Clients.ApiScopes)
            .AddInMemoryIdentityResources(Clients.GetIdentityResources())
            .AddDeveloperSigningCredential();

            services.AddAuthorization();

            services.AddScoped<ISeed, AuthenticationSeed>();



            return services;
        }

        public static IApplicationBuilder UseAuthenticationModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
            //dbContext.Database.Migrate();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
