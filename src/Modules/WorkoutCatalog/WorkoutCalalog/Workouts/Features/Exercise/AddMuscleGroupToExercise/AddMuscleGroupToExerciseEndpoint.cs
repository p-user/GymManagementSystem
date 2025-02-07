

using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.Exercise.AddMuscleGroupToExercise
{
    public class AddMuscleGroupToExerciseEndpoint : ICarterModule
    {
       public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPost("/exercises/{exerciseId}/muscleGroups", async ([FromRoute]Guid exerciseId,[FromBody] Guid MuscleGroupId,ISender sender, CancellationToken cancellationToken) =>
            {
               var command = new AddMuscleGroupToExerciseCommand(exerciseId, MuscleGroupId);
                var result = await sender.Send(command, cancellationToken);
                return result is not null ? Results.Ok(result) : Results.NotFound();    
            })
                .Produces<ViewExerciseDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .Produces(StatusCodes.Status400BadRequest)
                .WithDescription("Add muscle group to exercise")
                .WithTags("Exercise")
                .WithSummary("Add muscle group to exercise")
                ;
        }
    }
    
}
