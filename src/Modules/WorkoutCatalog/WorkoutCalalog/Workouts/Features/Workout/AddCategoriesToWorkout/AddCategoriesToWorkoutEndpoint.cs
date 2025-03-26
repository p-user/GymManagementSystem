

using WorkoutCatalog.Workouts.Features.Workout.UpdateWorkoutCategories;

namespace WorkoutCatalog.Workouts.Features.Workout.AddcategoriesToWorkout
{
    public class AddCategoriesToWorkoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/workouts/{workoutId}/categories", AddCategoriesToWorkout)
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Add workout categories to workout")
            .WithSummary("Add workout categories to workout")
            .WithTags("Workouts");

        }

        private async Task<IResult> AddCategoriesToWorkout(ISender sender, [FromRoute] Guid workoutId, [FromBody] List<Guid> categoryIds)
        {
            var command = new AddCategoriesToWorkoutCommand(workoutId, categoryIds);
            var result = await sender.Send(command);
            return result.Match(result => Microsoft.AspNetCore.Http.Results.Ok(result), ApiResults.Problem);
        }
    }
}
