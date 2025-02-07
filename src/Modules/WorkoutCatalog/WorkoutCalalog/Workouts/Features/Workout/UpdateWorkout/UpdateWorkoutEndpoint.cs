
using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout
{
    public class UpdateWorkoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/workout/{id:guid}", async (ISender sender, [FromRoute] Guid id, [FromBody] UpdateWorkoutDto dto) =>
            {
                var response = await sender.Send(new UpdateWorkoutCommand(dto));
                return response != Guid.Empty ? Results.Ok(response) : Results.InternalServerError();
            })
                .WithTags("Workouts")
                .WithSummary("Update a specific workout routine using its unique identifier")
                .WithName("UpdateWorkout")
                .WithDescription("Updates a workout in the workout catalog.")
                .Produces<Guid>()
                .Produces<UpdateWorkoutDto>();
        }
    }

}
