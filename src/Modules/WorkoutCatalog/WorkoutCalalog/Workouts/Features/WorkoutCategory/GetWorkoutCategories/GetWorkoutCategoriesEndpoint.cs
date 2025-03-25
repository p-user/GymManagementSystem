namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategories
{
    public class GetWorkoutCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/workout/categories", GetWorkoutCategories)
                .WithDescription("Retrieves all workout categories for the gym's workout catalog.")
                .WithSummary("Get all workout categories.")
                .Produces<List<ViewWorkoutCategoryDto>>(StatusCodes.Status200OK)
                .WithName("GetWorkoutCategories")
                .WithTags("Workout Categories");
        }

        private async Task<IResult> GetWorkoutCategories(ISender sender)
        {
            var response = await sender.Send(new GetWorkoutCategoriesQuery());
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);

        }
    }
}