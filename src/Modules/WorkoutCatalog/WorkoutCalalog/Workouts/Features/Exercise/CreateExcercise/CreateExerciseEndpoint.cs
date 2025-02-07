

using Microsoft.AspNetCore.Http.HttpResults;

namespace WorkoutCatalog.Workouts.Features.Exercise.CreateExcercise
{
    public class CreateExerciseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/exercise", async (CreateExerciseDto dto, CancellationToken ct, ISender sender) =>
             {
                 var command = new CreateExcerciseCommand(dto);
                 var response = await sender.Send(command, ct);
                 return Results.Created($"/exercise/{response}", response);
             })
              .WithName("CreateExercise")
             .WithTags("Exercise")
             .Produces<Guid>(StatusCodes.Status201Created)
             .ProducesValidationProblem(StatusCodes.Status400BadRequest)
             .WithDescription("Creates a new exercise in the workout catalog")
             .WithSummary("Create a new exercise for the workout catalog");
        }
    }
}
