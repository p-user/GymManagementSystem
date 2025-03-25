namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategoryById
{
    public class GetWorkoutCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/workout/categories/{id:guid}", GetWorkoutCategoryById)
                .WithTags("Workout Categories")
                .WithSummary("Get a specific workout category by ID")
                .WithDescription("Retrieves a workout category by its unique identifier.")
                .WithName("GetWorkoutCategoryById")
                .Produces<ViewWorkoutCategoryDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }

        private async Task<IResult> GetWorkoutCategoryById(ISender sender, [FromRoute] Guid id)
        {
            var response = await sender.Send(new GetWorkoutCategoryByIdQuery(id));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}