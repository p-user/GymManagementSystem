
using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.UpdateMuscleGroup
{
    public class UpdateMuscleGroupEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/exercise/muscle-groups/{id:guid}", async (ISender sender, [FromRoute] Guid id, UpdateMuscleGroupDto dto) =>
            {
                var response = await sender.Send(new UpdateMuscleGroupCommand(id, dto));
                return response != Guid.Empty ? Results.Ok(response) : Results.NotFound();
            })
                .WithTags("Muscle Groups")
                .WithName("UpdateMuscleGroup")
                .WithSummary("Update a specific muscle group using its unique identifier.")
                .WithDescription("Updates a muscle group in the workout catalog using its unique identifier.")
                .Produces<Guid>().Produces<ViewMuscleGroupDto>();
        }
    }

}
