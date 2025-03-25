namespace WorkoutCatalog.Workouts.Features.MuscleGroups.UpdateMuscleGroup
{
    public class UpdateMuscleGroupEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/exercise/muscle-groups/{id:guid}", UpdateMuscleGroup)
                .WithTags("Muscle Groups")
                .WithName("UpdateMuscleGroup")
                .WithSummary("Update a specific muscle group using its unique identifier.")
                .WithDescription("Updates a muscle group in the workout catalog using its unique identifier.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }

        private async Task<IResult> UpdateMuscleGroup(
            ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateMuscleGroupDto dto)
        {
            var response = await sender.Send(new UpdateMuscleGroupCommand(id, dto));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);
        }
    }
}