namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.CreateWorkoutCategory
{
    public class CreateWorkoutCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/workout/categories", CreateWorkoutCategory)
                .WithName("CreateWorkoutCategory")
                .WithTags("Workout Categories")
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Creates a new workout category for the gym's workout catalog.")
                .WithSummary("Create a new workout category.");
        }

        private async Task<IResult> CreateWorkoutCategory(
            CreateWorkoutCategoryDto dto,
            CancellationToken ct,
            ISender sender)
        {
            var command = new CreateWorkoutCategoryCommand(dto);
            var result = await sender.Send(command, ct);
            return result.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);

        }
    }
}