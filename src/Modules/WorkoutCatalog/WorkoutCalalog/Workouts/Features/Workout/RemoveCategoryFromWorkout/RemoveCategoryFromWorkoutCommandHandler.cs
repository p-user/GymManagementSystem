﻿namespace WorkoutCatalog.Workouts.Features.Workout.RemoveCategoryFromWorkout
{
    public record RemoveWorkoutCategoryCommand(Guid workoutId, Guid categoryId) : IRequest<Results<Guid>>;
    public class RemoveCategoryFromWorkoutCommandHandler(WorkoutCatalogDbContext _context) : IRequestHandler<RemoveWorkoutCategoryCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(RemoveWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            var workout = await _context.Workouts.Include(s => s.WorkoutCategories).FirstOrDefaultAsync(s => s.Id.Equals(request.workoutId));
            var category = await _context.WorkoutCategories.FindAsync(request.categoryId, cancellationToken);
            if (workout == null)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.WorkoutCategoryErrors.NotFound(request.categoryId));
            }
            workout.RemoveWorkoutCategory(category);
            await _context.SaveChangesAsync();
            return workout.Id;
        }
    }
}
