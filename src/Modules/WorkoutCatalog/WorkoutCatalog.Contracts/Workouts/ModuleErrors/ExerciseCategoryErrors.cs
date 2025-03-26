
using Shared.Exceptions;

namespace WorkoutCatalog.Contracts.Workouts.ModuleErrors
{
    public static class ExerciseCategoryErrors
    {

        public static Error NotFound(Guid categoryId) =>
        Error.NotFound("ExerciseCategory.NotFound", $"The exercise category with the identifier {categoryId} was not found");

        public static Error DeleteProblem() =>
       Error.Problem("ExerciseCategory.AssociatedWithExercise", $"The exercise category can not be deleted. At least one exercise is associated with this category");
    }
}
