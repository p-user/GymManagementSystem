var builder = DistributedApplication.CreateBuilder(args);


//containerize the database and add it here
var dbContainer = builder.AddSqlServer("DefaultConnection") // manage a sql server container with the given name, named aftre the connection string
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var database = dbContainer.AddDatabase("GymMS");



// Add  Web API project
var apiService = builder.AddProject<Projects.GymMS_Api>("gym-api")
.WithReference(database)
.WaitFor(database);

builder.Build().Run();
