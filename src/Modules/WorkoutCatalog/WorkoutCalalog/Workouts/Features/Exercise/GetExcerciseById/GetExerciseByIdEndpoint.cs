
using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.Exercise.GetExcerciseById
{
    public class GetExerciseByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/excercise/{id:guid}", async ([FromRoute] Guid id, ISender sender) =>
            {
                var response = await sender.Send(new GetExerciseByIdQuery(id));
                return response is not null ? Results.Ok(response) : Results.NotFound();
            })
                .WithName("GetExerciseById")
                .WithTags("Exercise")
                .Produces<ViewExerciseDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Retrieves an exercise by its unique identifier.")
                .WithSummary("Get a specific exercise by ID.");
        }
    }

}
