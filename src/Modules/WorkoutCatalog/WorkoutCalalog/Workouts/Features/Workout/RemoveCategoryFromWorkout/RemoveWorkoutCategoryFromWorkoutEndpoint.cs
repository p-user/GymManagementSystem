


namespace WorkoutCatalog.Workouts.Features.Workout.RemoveCategoryFromWorkout
{
    public class RemoveWorkoutCategoryFromWorkoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/workouts/{workoutId}/categories/{categoryId}", RemoveCategoryFromWorkout)
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithDescription("Remove workout category from workout")
                .WithSummary("Remove workout category from workout")
                .WithTags("Workouts");

        }

        private async Task<IResult> RemoveCategoryFromWorkout(ISender sender, [FromRoute] Guid workoutId, [FromRoute] Guid categoryId)
        {
            var command = new RemoveWorkoutCategoryCommand(workoutId, categoryId);
            var result = await sender.Send(command);
            return result.Match(result => Microsoft.AspNetCore.Http.Results.Ok(result), ApiResults.Problem);
        }
    }
}
