
namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout
{
    public class UpdateWorkoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/workout/{id:guid}", UpdateWorkout)
                .WithTags("Workouts")
                .WithSummary("Update a specific workout routine using its unique identifier")
                .WithName("UpdateWorkout")
                .WithDescription("Updates a workout in the workout catalog.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }

        private async Task<IResult> UpdateWorkout(
            ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateWorkoutDto dto)
        {
            var response = await sender.Send(new UpdateWorkoutCommand(dto));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);

        }
    }
}