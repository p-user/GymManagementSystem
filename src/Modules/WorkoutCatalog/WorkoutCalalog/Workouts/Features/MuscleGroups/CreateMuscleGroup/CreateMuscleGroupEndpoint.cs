namespace WorkoutCatalog.Workouts.Features.MuscleGroups.CreateMuscleGroup
{
    public class CreateMuscleGroupEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/exercise/muscle-groups", CreateMuscleGroup)
                .WithName("CreateMuscleGroup")
                .WithTags("Muscle Groups")
                .Produces<Results<Guid>>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Creates a new muscle group for the workout catalog.")
                .WithSummary("Create a new muscle group.");
        }

        private async Task<IResult> CreateMuscleGroup(
            CreateMuscleGroupDto request,
            CancellationToken ct,
            ISender sender)
        {
            var command = new CreateMuscleGroupCommand(request);
            var response = await sender.Send(command, ct);
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}