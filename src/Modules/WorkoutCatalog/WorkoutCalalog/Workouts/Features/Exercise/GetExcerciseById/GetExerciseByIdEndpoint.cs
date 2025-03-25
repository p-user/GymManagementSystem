using WorkoutCatalog.Workouts.Features.Exercise.GetExcerciseById;

namespace WorkoutCatalog.Workouts.Features.Exercise.GetExerciseById
{
    public class GetExerciseByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise/{id:guid}", GetExerciseById)
                .WithName("GetExerciseById")
                .WithTags("Exercise")
                .Produces<ViewExerciseDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Retrieves an exercise by its unique identifier.")
                .WithSummary("Get a specific exercise by ID.");
        }

        private async Task<IResult> GetExerciseById([FromRoute] Guid id, ISender sender)
        {
            var response = await sender.Send(new GetExerciseByIdQuery(id));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);

        }
    }
}