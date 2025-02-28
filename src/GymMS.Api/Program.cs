
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddWorkoutCatalogData(builder.Configuration);
builder.Services.AddMembershipData(builder.Configuration);
builder.Services.AddStaffData(builder.Configuration);




builder.Services.AddOpenApi();
builder.Services.AddCors();

//gwet the assemblies of the modules

var workoutCatalogModule = typeof(WorkoutCatalogServiceExtensions).Assembly;
var membershipModule = typeof(MembershipServiceExtensions).Assembly;
var staffModule = typeof(StaffServiceExtensions).Assembly;



//carter config
builder.Services.AddCarter(workoutCatalogModule, membershipModule, staffModule);

//mediatR
builder.Services.AddMediatR(workoutCatalogModule, membershipModule, staffModule);

//validations
builder.Services.AddValidatorsFromAssemblies([workoutCatalogModule, membershipModule, staffModule]);

//automapper

builder.Services.AddAutoMapper([workoutCatalogModule, membershipModule, staffModule]);




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapCarter();



//consider to change the middleware order --> which migrations should apply first 
//remeber : a workout or exercise should be associated with a staff member 
app.UseStaffModule();
app.UseWorkoutCatalogModule();
app.UseMembershipModule();




app.Run();

// Required for integration testing with WebApplicationFactory
public partial class Program { }

