namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.CreateExerciseCategory
{
    public class CreateExerciseCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/exercise/categories", CreateExerciseCategory)
                .WithName("CreateExerciseCategory")
                .WithTags("Exercise Categories")
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Creates a new exercise category for the workout catalog.")
                .WithSummary("Create a new exercise category.");
        }

        private async Task<IResult> CreateExerciseCategory(
            CreateExerciseCategoryDto request,
            CancellationToken cancel,
            ISender sender)
        {
            var command = new CreateExerciseCategoryCommand(request);
            var response = await sender.Send(command, cancel);
            return Results.Created($"/exercise/exercise-categories/{response}", response);
        }
    }
}