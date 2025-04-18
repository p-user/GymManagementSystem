﻿namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategoryById
{
    public class GetExerciseCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exercise/categories/{id:guid}", GetExerciseCategoryById)
                .WithDescription("Retrieves an exercise category by its unique identifier.")
                .WithSummary("Get a specific exercise category by ID")
                .WithName("GetExerciseCategoryById")
                .Produces<Guid>().Produces<ViewExerciseDto>()
                .WithTags("Exercise Categories");
        }

        private async Task<IResult> GetExerciseCategoryById(ISender sender, [FromRoute] Guid id)
        {
            var response = await sender.Send(new GetExerciseCategoryByIdQuery(id));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);

        }
    }

}
