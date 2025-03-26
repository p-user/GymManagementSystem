
using Shared.Exceptions;

namespace WorkoutCatalog.Contracts.Workouts.ModuleErrors
{
    public static class MuscleGroupError
    {
        public static Error NotFound(Guid musclegroupId) =>
           Error.NotFound("Muscle.NotFound", $"The workout with the identifier {musclegroupId} was not found");

        public static Error NameConflict(string muscle) =>
           Error.Conflict("Muscle.NameConflict", $"Another muscle group  with the name {muscle} was found");

        public static Error InvalidMuscleGroup() =>
              Error.Problem("Muscle.InvalidMuscleGroup", $"Invalid muscle group");

        public static Error DeleteConflict() =>
          Error.Conflict("Muscle.AssociatedWithExercise", $"This muscle group is associated with at least one exercise!");
    }
}
