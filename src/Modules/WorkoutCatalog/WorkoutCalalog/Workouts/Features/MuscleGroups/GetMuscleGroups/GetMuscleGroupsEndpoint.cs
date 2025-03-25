
namespace WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroups
{
    public class GetMuscleGroupsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise/muscle-groups", GetMuscleGroups)
                .WithName("GetMuscleGroups")
                .WithTags("Muscle Groups")
                .Produces<List<ViewMuscleGroupDto>>(StatusCodes.Status200OK)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Retrieves all muscle groups for the gym's workout catalog.")
                .WithSummary("Get all muscle groups.");
        }

        private async Task<IResult> GetMuscleGroups(ISender sender)
        {
            var response = await sender.Send(new GetMuscleGroupsQuery());
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);

        }
    }
}