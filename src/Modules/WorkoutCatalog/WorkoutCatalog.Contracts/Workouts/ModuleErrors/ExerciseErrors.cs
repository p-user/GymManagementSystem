using Shared.Exceptions;

namespace WorkoutCatalog.Contracts.Workouts.ModuleErrors
{
    public static class ExerciseErrors
    {
        public static Error NotFound(Guid exerciseId) =>
      Error.NotFound("Exercise.NotFound", $"The exercise with the identifier {exerciseId} was not found");


        public static Error CanNotBeDleted(Guid exerciseId) =>
     Error.Problem("Exercise.AssociatedWithWorkout", $"The exercise with the identifier {exerciseId} can not be deleted as it belongs to a workout routine");

        public static Error NoValidMuscleGroups() =>
               Error.Problem("Exercise.NoValidMuscleGroups", $"Exercise should have at least one working group muscle");

        public static Error NoValidCategories() =>
               Error.Problem("Exercise.NoValidCategories", $"Exercise should have at least onevalid category");
    }
}
