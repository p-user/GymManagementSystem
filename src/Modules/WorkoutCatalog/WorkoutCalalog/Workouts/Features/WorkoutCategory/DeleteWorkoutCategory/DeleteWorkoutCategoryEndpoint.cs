namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.DeleteWorkoutCategory
{
    public class DeleteWorkoutCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/workout/categories/{id:guid}", DeleteWorkoutCategory)
                .WithName("DeleteWorkoutCategory")
                .WithTags("Workout Categories")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Deletes a workout category from the workout catalog using its unique identifier.")
                .WithSummary("Delete a workout category by its ID.");
        }

        private async Task<IResult> DeleteWorkoutCategory(ISender sender, [FromRoute] Guid id)
        {
            var response = await sender.Send(new DeleteWorkoutCategoryCommand(id));
            return response.Match(() => Microsoft.AspNetCore.Http.Results.Ok(), ApiResults.Problem);

        }
    }
}