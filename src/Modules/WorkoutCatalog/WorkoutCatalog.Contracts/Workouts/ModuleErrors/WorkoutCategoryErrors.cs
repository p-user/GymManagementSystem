
using Shared.Exceptions;

namespace WorkoutCatalog.Contracts.Workouts.ModuleErrors
{
    public static class WorkoutCategoryErrors
    {
        public static Error NotFound(Guid categoryId) =>
      Error.NotFound("WorkoutCategory.NotFound", $"The workout category with the identifier {categoryId} was not found");

        public static Error NameConflict(string categoryId) =>
      Error.Conflict("WorkoutCategory.NameExists", $"Another workout category with the name {categoryId} was  found");

        public static Error DeleteConflict() =>
         Error.Conflict("WorkoutCategory.AssociatedWithExercise", $"This WorkoutCategory is associated with at least one workout!");
    }
}
