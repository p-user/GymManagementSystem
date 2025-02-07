
namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategories
{
    public class GetExerciseCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise/categories", async (ISender sender, CancellationToken ct) =>
            {
                var response = await sender.Send(new GetExerciseCategoriesQuery(), ct);
                return Results.Ok(response);
            })
                .WithName("GetExerciseCategories")
                .WithTags("Exercise Categories")
                .Produces<List<ViewExerciseCategoryDto>>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Retrieves all exercise categories for the gym's workout catalog.")
                .WithSummary("Get all exercise categories.");
        }
    }
}
