

using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategoryById
{
    public class GetWorkoutCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/workout/categories/{id:guid}", async (ISender sender, [FromRoute] Guid id) =>
            {
                var response = await sender.Send(new GetWorkoutCategoryByIdQuery(id));
                return response is not null ? Results.Ok(response) : Results.NotFound();
            })
                 .WithTags("Workout Categories")
                .WithSummary("Get a specific workout category by ID")
                .WithDescription("Retrieves a workout category by its unique identifier.")
                .WithName("GetWorkoutCategoryById")
                .Produces<ViewWorkoutCategoryDto>();
        }
    }
}
