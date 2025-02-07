﻿

using Microsoft.AspNetCore.Mvc;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.UpdateExerciseCategory
{
    public class UpdateExerciseCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/exercise/categories/{id:guid}", async (ISender sender, [FromRoute] Guid id, CreateExerciseCategoryDto request) =>
            {
                var command = new UpdateExerciseCategoryCommand(request, id);
                var response = await sender.Send(command);
                return response ? Results.NoContent() : Results.NotFound();
            })
              .WithName("UpdateExerciseCategory")
              .WithTags("Exercise Categories")
              .Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status404NotFound)
              .ProducesValidationProblem(StatusCodes.Status400BadRequest)
              .WithDescription("Updates an exercise category in the gym's workout catalog by its unique identifier.")
              .WithSummary("Update an exercise category by ID.");

        }
        //get all exercices eksik
    }

}
