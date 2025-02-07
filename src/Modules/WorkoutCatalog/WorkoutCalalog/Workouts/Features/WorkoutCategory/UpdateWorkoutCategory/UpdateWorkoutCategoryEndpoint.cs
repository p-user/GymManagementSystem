
using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.UpdateWorkoutCategory
{
    public class UpdateWorkoutCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/workout/categories/{id:guid}", async (ISender sender, [FromRoute] Guid id, [FromBody] UpdateWorkoutCategoryDto dto) =>
            {
                dto.Id = id;
                var response = await sender.Send(new UpdateWorkoutCategoryCommand(dto));
                return response != Guid.Empty ? Results.Ok(response) : Results.InternalServerError();
            })
                 .WithTags("Workout Categories")
                .WithName("UpdateWorkoutCategory")
                .WithSummary("Update a specific workout category using its unique identifier")
                .WithDescription("Updates a workout category in the workout catalog.")
                .Produces<Guid>()
                .Produces<UpdateWorkoutCategoryDto>();
        }
    }

}
