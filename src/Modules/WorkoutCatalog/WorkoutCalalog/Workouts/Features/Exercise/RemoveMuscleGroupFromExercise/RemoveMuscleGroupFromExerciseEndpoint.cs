

using Microsoft.AspNetCore.Mvc;
using WorkoutCatalog.Workouts.Features.Exercise.RemoceMuscleGroupFromExercise;

namespace WorkoutCatalog.Workouts.Features.Exercise.RemoveMuscleGroupFromExercise
{
    public class RemoveMuscleGroupFromExerciseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/exercises/{exerciseId}/muscleGroups/{muscleGroupId}", async ([FromRoute] Guid exerciseId, [FromRoute] Guid muscleGroupId, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new RemoveMuscleGroupFromExerciseCommand(exerciseId, muscleGroupId);
                var result = await sender.Send(command, cancellationToken);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
                .Produces<ViewExerciseDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .Produces(StatusCodes.Status400BadRequest)
                .WithDescription("Remove muscle group from exercise")
                .WithTags("Exercise")
                .WithSummary("Remove muscle group from exercise")
                ;
        }
    }
}
