
using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategoryById
{
    public class GetExerciseCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise/categories/{id:guid}", async (ISender sender, [FromRoute] Guid id) =>
            {
                var response = await sender.Send(new GetExerciseCategoryByIdQuery(id));
                return response is not null ? Results.Ok(response) : Results.NotFound();
            })
                .WithDescription("Retrieves an exercise category by its unique identifier.")
                .WithSummary("Get a specific exercise category by ID")
                .WithName("GetExerciseCategoryById")
                .Produces<Guid>().Produces<ViewExerciseDto>()
                .WithTags("Exercise Categories");
        }
    }

}
