namespace WorkoutCatalog.Workouts.Features.Exercise.DeleteExercise
{
    public class DeleteExerciseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/exercise/{id:guid}", DeleteExercise)
                .WithName("DeleteExercise")
                .WithTags("Exercise")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Deletes an exercise from the workout catalog using its unique identifier.")
                .WithSummary("Delete an exercise by its ID.");
        }

        private async Task<IResult> DeleteExercise([FromRoute] Guid id, CancellationToken ct, ISender sender)
        {
            var result = await sender.Send(new DeleteExerciseCommand(id), ct);
            return result.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);
        }
    }
}