namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout
{
    public record UpdateWorkoutCommand(UpdateWorkoutDto dto) : IRequest<Guid>;
    public class UpdateworkoutCommandHandler
    {
    }
}
