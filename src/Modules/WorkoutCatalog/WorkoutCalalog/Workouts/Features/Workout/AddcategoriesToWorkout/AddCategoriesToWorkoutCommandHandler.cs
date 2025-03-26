
namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkoutCategories
{

    public record AddCategoriesToWorkoutCommand(Guid WorkoutId, List<Guid> CategoryIds) : IRequest<Results<Guid>>;
    public class AddCategoriesToWorkoutCommandHandler(WorkoutCatalogDbContext workoutCatalogDbContext) : IRequestHandler<AddCategoriesToWorkoutCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(AddCategoriesToWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workout = await workoutCatalogDbContext.Workouts.Include(s => s.WorkoutCategories).FirstOrDefaultAsync(s => s.Id == request.WorkoutId, cancellationToken);
            if (workout == null)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.WorkoutErrors.NotFound(request.WorkoutId));
            }

            var categories = await workoutCatalogDbContext.WorkoutCategories.Where(x => request.CategoryIds.Contains(x.Id)).ToListAsync(cancellationToken);

            if (categories.Count == 0)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.WorkoutErrors.InvalidCategories());
            }

            workout.AddWorkoutCategories(categories);
            await workoutCatalogDbContext.SaveChangesAsync(cancellationToken);
            return workout.Id;
        }
    }

}
