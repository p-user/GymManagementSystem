
using Authentication.Data;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddWorkoutCatalogData(builder.Configuration);
builder.Services.AddMembershipData(builder.Configuration);
builder.Services.AddStaffData(builder.Configuration);
builder.Services.AddAuthenticationData(builder.Configuration);

//builder.Services.AddSingleton<DatabaseSeeder>();




builder.Services.AddOpenApi();
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




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapCarter();



//consider to change the middleware order --> which migrations should apply first 
//remeber : a workout or exercise should be associated with a staff member 
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

