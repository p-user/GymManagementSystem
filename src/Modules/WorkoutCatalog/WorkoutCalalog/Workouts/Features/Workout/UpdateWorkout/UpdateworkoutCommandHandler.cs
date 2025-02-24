
namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout
{
    public record UpdateWorkoutCommand(UpdateWorkoutDto dto) : IRequest<Guid>;
    public class UpdateworkoutCommandHandler(WorkoutCatalogDbContext workoutCatalogDbContext) : IRequestHandler<UpdateWorkoutCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
        {
            var entity = await workoutCatalogDbContext.Workouts.FindAsync(request.dto.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Workout not found");
            }
            entity.Update(request.dto.Name, request.dto.Description);
            await workoutCatalogDbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }

       
    }
    
}