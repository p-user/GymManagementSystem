namespace WorkoutCatalog.Workouts.Features.Workout.CreateWorkout
{
    public class CreateWorkoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/workouts", CreateWorkout)
                .WithName("CreateWorkout")
                .WithTags("Workouts")
                .Produces<int>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Creates a new workout routine for the gym's workout catalog.")
                .WithSummary("Create a new workout routine.");
        }

        private async Task<IResult> CreateWorkout(
            CreateWorkoutDto dto,
            CancellationToken ct,
            ISender sender)
        {
            var command = new CreateWorkoutCommand(dto);
            var response = await sender.Send(command, ct);
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}