using Carter;
using Membership.Data;
using Scalar.AspNetCore;
using Shared.Extensions;
using WorkoutCalalog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddWorkoutCatalogData(builder.Configuration);
builder.Services.AddMembershipData(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddCors();

//gwet the assemblies of the modules

var workoutCatalogModule = typeof(WorkoutCatalogServiceExtensions).Assembly;



//carter config
builder.Services.AddCarter(workoutCatalogModule);

//mediatR
builder.Services.AddMediatR(workoutCatalogModule);

//validations




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapCarter();

app.UseWorkoutCatalogModule();




app.Run();

// Required for integration testing with WebApplicationFactory
public partial class Program { }

