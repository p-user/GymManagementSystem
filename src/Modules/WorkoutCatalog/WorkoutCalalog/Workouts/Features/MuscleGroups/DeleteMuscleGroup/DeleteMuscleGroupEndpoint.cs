using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.DeleteMuscleGroup
{
    public class DeleteMuscleGroupEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/exercise/muscle-groups/{id:guid}", async (ISender sender, [FromRoute] Guid id) =>
            {
                var response = await sender.Send(new DeleteMuscleGroupCommand(id));
                return response ? Results.NoContent() : Results.NotFound();
            })
                .WithName("DeleteMuscleGroup")
                .WithTags("Muscle Groups")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Deletes a muscle group from the workout catalog using its unique identifier.")
                .WithSummary("Delete a muscle group by its ID.");
        }
    }

}
