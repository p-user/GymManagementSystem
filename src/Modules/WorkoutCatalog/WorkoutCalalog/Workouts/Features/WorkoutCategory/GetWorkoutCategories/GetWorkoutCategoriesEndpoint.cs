

namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategories
{
    public class GetWorkoutCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/workout/categories", async (ISender sender) =>
            {
                var response = await sender.Send(new GetWorkoutCategoriesQuery());
                return Results.Ok(response);
            })
                .WithDescription("Retrieves all workout categories for the gym's workout catalog.")
                .WithSummary("Get all workout categories.")
                .Produces<List<ViewWorkoutCategoryDto>>(StatusCodes.Status200OK)
                .WithName("workout-categories")
                 .WithTags("Workout Categories");
        }
    }
}
