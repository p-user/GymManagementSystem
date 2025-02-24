using Microsoft.AspNetCore.Mvc;
using WorkoutCatalog.Workouts.Dtos;
using WorkoutCatalog.Workouts.Features.Exercise.UpdateExcercise;

namespace WorkoutCatalog.Workouts.Features.Exercise.UpdateExercise
{
    public class UpdateExerciseEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/exercise/{id:guid}", UpdateExercise)
                .WithName("UpdateExercise")
                .WithTags("Exercise")
                .WithSummary("Update an exercise by its ID.")
                .WithDescription("Updates an exercise in the workout catalog using its unique identifier.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }

        private async Task<IResult> UpdateExercise(
            ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateExerciseDto dto)
        {
            var response = await sender.Send(new UpdateExerciseCommand(dto, id));
            return response != Guid.Empty ? Results.NoContent() : Results.NotFound();
        }
    }
}