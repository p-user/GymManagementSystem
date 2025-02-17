

namespace WorkoutCatalog.Workouts.Features.Workout.RemoveCategoryFromWorkout
{
    public record RemoveWorkoutCategoryCommand(Guid workoutId, Guid categoryId ) : IRequest<Guid>;
    public class RemoveCategoryFromWorkoutCommandHandler(WorkoutCatalogDbContext _context) : IRequestHandler<RemoveWorkoutCategoryCommand, Guid>
    {
        public async Task<Guid> Handle(RemoveWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            var workout = await _context.Workouts.Include(s=>s.WorkoutCategories).FirstOrDefaultAsync(s=>s.Id.Equals(request.workoutId));
            var category = await _context.WorkoutCategories.FindAsync(request.categoryId, cancellationToken);
            if(workout == null)
            {
                throw new ArgumentException("Category does not exist");
            }
            workout.RemoveWorkoutCategory(category);
            await _context.SaveChangesAsync();
            return workout.Id;
        }
    }
}
