
using Authentication.Models;
using Authentication.Services;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Data
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddAuthenticationData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IDiscoveryService, DiscoveryService>();

            services.AddIdentity<Models.User, Models.Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
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
            .AddInMemoryApiResources((IEnumerable<Duende.IdentityServer.Models.ApiResource>)Clients.GetIdentityResources())
            .AddDeveloperSigningCredential();




            return services;
        }

        public static IApplicationBuilder UseAuthenticationModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //dbContext.Database.Migrate();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            return app;
        }
    }
}
