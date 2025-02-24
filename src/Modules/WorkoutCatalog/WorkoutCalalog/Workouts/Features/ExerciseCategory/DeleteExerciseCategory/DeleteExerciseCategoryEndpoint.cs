using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.DeleteExerciseCategory
{
    public class DeleteExerciseCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/exercise/categories/{id:guid}", DeleteExerciseCategory)
                .WithName("DeleteExerciseCategory")
                .WithTags("Exercise Categories")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Deletes an exercise category from the workout catalog using its unique identifier.")
                .WithSummary("Delete an exercise category by its ID.");
        }

        private async Task<IResult> DeleteExerciseCategory(ISender sender, [FromRoute] Guid id)
        {
            var response = await sender.Send(new DeleteExcerciseCategoryCommand(id));
            return response ? Results.NoContent() : Results.NotFound();
        }
    }
}