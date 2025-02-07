namespace WorkoutCatalog.Workouts.Features.Workout.CreateWorkout
{
    public class CreateWorkoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/workouts", async (CreateWorkoutDto dto, CancellationToken ct, ISender sender) =>
            {
                var command = new CreateWorkoutCommand(dto);
                var response = await sender.Send(command, ct);
                return Results.Created($"/workouts/{response}", response);
            })
                .WithName("CreateWorkout")
                .WithTags("Workouts")
                .Produces<int>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Creates a new workout routine for the gym's workout catalog.")
                .WithSummary("Create a new workout routine.");
        }
    }
}
