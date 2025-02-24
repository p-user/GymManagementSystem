namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategories
{
    public class GetExerciseCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise/categories", GetExerciseCategories)
                .WithDescription("Retrieves all exercise categories for the gym's workout catalog.")
                .WithName("GetExerciseCategories")
                .WithTags("Exercise Categories")
                .WithSummary("Get all exercise categories.")
                .Produces<List<ViewExerciseCategoryDto>>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }

        private async Task<IResult> GetExerciseCategories(ISender sender, CancellationToken ct)
        {
            var response = await sender.Send(new GetExerciseCategoriesQuery(), ct);
            return Results.Ok(response);
        }
    }
}