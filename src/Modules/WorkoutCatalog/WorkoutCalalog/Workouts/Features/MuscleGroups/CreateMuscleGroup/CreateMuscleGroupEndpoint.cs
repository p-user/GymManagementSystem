﻿namespace WorkoutCatalog.Workouts.Features.MuscleGroups.CreateMuscleGroup
{
    public class CreateMuscleGroupEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/exercise/muscle-groups", async (CreateMuscleGroupDto request, CancellationToken ct, ISender sender) =>
            {
                var command = new CreateMuscleGroupCommand(request);
                var response = await sender.Send(command, ct);
                return Results.Created($"/exercise/muscle-groups/{response}", response);
            })
             .WithName("CreateMuscleGroup")
            .WithTags("Muscle Groups")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Creates a new muscle group for the workout catalog.")
            .WithSummary("Create a new muscle group.");
        }
    }
}
