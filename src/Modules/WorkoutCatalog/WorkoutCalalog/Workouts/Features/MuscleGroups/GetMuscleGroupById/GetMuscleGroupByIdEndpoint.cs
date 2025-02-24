using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroupById
{
    public class GetMuscleGroupByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/workout/muscle-groups/{id:guid}", GetMuscleGroupById)
                .WithDescription("Retrieves a muscle group by its unique identifier.")
                .WithSummary("Get a specific muscle group by ID")
                .WithName("GetMuscleGroupById")
                .WithTags("Muscle Groups")
                .Produces<ViewMuscleGroupDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }

        private async Task<IResult> GetMuscleGroupById(ISender sender, [FromRoute] Guid id)
        {
            var response = await sender.Send(new GetMuscleGroupByIdQuery(id));
            return response is not null ? Results.Ok(response) : Results.NotFound();
        }
    }
}