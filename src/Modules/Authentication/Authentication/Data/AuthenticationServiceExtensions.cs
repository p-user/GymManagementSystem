

namespace Authentication.Data
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddAuthenticationData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptors>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AuthenticationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddScoped<IDiscoveryService, DiscoveryService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IRequestHandler<RegisterUserCommand<CreateMemberDto>, RegisterUserCommandResponse>, RegisterUserCommandHandler<CreateMemberDto>>();
            services.AddScoped<IRequestHandler<RegisterUserCommand<CreateStaffDto>, RegisterUserCommandResponse>, RegisterUserCommandHandler<CreateStaffDto>>();


            services.AddScoped<ISeed, AuthenticationSeed>();

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
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>()  // Custom claims enrichment
            .AddInMemoryClients(Clients.GetClients())
            .AddInMemoryApiScopes(Clients.ApiScopes)
            .AddInMemoryApiResources(Clients.ApiResources)
            .AddInMemoryIdentityResources(Clients.GetIdentityResources())
            .AddDeveloperSigningCredential();

      

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration.GetSection("IdentityServer").Value;
                options.Audience = configuration.GetSection("IdentityServer").Value;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["IdentityServer"],
                    ValidateAudience = true,
                    ValidAudience = "GYMApi"
                };
               
            }); 
            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder UseAuthenticationModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
            dbContext.Database.Migrate();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();


            return app;
        }
    }
}
