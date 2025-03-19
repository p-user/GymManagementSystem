


using Shared.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration);
       
});

builder.Services.AddWorkoutCatalogData(builder.Configuration);
builder.Services.AddMembershipData(builder.Configuration);
builder.Services.AddStaffData(builder.Configuration);
builder.Services.AddAuthenticationData(builder.Configuration);
builder.Services.AddHttpClient();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();




builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
builder.Services.AddCors();

//gwet the assemblies of the modules

var workoutCatalogModule = typeof(WorkoutCatalogServiceExtensions).Assembly;
var membershipModule = typeof(MembershipServiceExtensions).Assembly;
var staffModule = typeof(StaffServiceExtensions).Assembly;
var authenticationModule = typeof(AuthenticationServiceExtensions).Assembly;



//carter config
builder.Services.AddCarter(workoutCatalogModule, membershipModule, staffModule, authenticationModule);

//mediatR
builder.Services.AddMediatR(workoutCatalogModule, membershipModule, staffModule, authenticationModule);

//validations
builder.Services.AddValidatorsFromAssemblies([workoutCatalogModule, membershipModule, staffModule, authenticationModule]);

//automapper

builder.Services.AddAutoMapper([workoutCatalogModule, membershipModule, staffModule, authenticationModule]);

//mass transit

builder.Services.AddMassTransitWithAssemblies([workoutCatalogModule, membershipModule, staffModule, authenticationModule]);




var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
                .WithTitle("Gym Management System")
                .WithTheme(ScalarTheme.Purple)
                .WithDownloadButton(true)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}
app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapCarter();



//consider to change the middleware order --> which migrations should apply first 
//remeber : a workout or exercise should be associated with a staff member 
app.UseExceptionHandler(opt => { });
app.UseAuthenticationModule();
app.UseStaffModule();
app.UseWorkoutCatalogModule();
app.UseMembershipModule();



//Todo : search for refactoring
using (var scope = app.Services.CreateScope())
{
    var seeder = new DatabaseSeeder(scope.ServiceProvider, scope.ServiceProvider.GetServices<ISeed>());
    seeder.SeedAsync().Wait();
}





app.Run();

// Required for integration testing with WebApplicationFactory
public partial class Program { }

