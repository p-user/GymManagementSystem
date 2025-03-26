
using Shared.Exceptions;

namespace WorkoutCatalog.Contracts.Workouts.ModuleErrors
{
    public static class WorkoutErrors
    {
        public static Error NotFound(Guid workoutId) =>
            Error.NotFound("Workout.NotFound", $"The workout with the identifier {workoutId} was not found");

        public static Error NameConflict(string workout) =>
           Error.Conflict("Workout.NameConflict", $"Another workout with the name {workout} was found");

        public static Error InvalidCategory(Guid category) =>
            Error.Problem("Workout.InvalidCategory", $"The category with the identifier {category} is invalid");

        public static Error InvalidExercise(Guid exercise) =>
            Error.Problem("Workout.InvalidExercise", $"The exercise with the identifier {exercise} is invalid");

        public static Error InvalidCategories() =>
           Error.Problem("Workout.InvalidCategories", $"The Workout should have at least one valid category");

        public static Error InvalidExercises() =>
          Error.Problem("Workout.InvalidExercises", $"The Workout should have at least one valid exercise");


    }
}
