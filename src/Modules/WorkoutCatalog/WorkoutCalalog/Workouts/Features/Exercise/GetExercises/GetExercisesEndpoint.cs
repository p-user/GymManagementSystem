
namespace WorkoutCatalog.Workouts.Features.Exercise.GetExercises
{
    public class GetExercisesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/execercise", async (ISender sender, CancellationToken ct) =>
            {
                var query = new GetExercisesQuery();
                var response = await sender.Send(query, ct);
                return Results.Ok(response);
            })
                .WithDescription("Get all exercises registered")
                .WithName("GetExercises")
                .WithTags("Exercise")
                .WithSummary("Get all exercises")
                .Produces<List<ViewExerciseDto>>(StatusCodes.Status200OK);
        }
    }
}
