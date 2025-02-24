using Microsoft.AspNetCore.Mvc;
using WorkoutCatalog.Workouts.Features.Exercise.CreateExercise;

namespace WorkoutCatalog.Workouts.Features.Exercise.CreateExercise
{
    public class CreateExerciseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/exercise", CreateExercise)
                .WithDescription("Creates a new exercise in the workout catalog")
                .WithName("CreateExercise")
                .WithTags("Exercise")
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new exercise for the workout catalog");
        }

        private async Task<IResult> CreateExercise(ISender sender,[FromBody]CreateExerciseDto dto, CancellationToken ct)
        {
            var command = new CreateExerciseCommand(dto);
            var response = await sender.Send(command, ct);
            return Results.Created($"/exercise/{response}", response);
        }
    }
}