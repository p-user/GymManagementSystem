namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.DeleteWorkoutCategory
{
    public record DeleteWorkoutCategoryCommand(Guid Id) : IRequest<bool>;
    public class DeleteWorkoutCategoryCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteWorkoutCategoryCommand, bool>
    {
        public async Task<bool> Handle(DeleteWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            var exists = await context.WorkoutCategories.FindAsync(request.Id, cancellationToken);
            if (exists == null)
            {
                throw new Exception("Workout category not found!");
            }

            var isDeletable = await context.WorkoutCategories.Include(w => w.Workouts)
                .AnyAsync(w => w.Id == request.Id && w.Workouts.Count > 0, cancellationToken);
            if (isDeletable)
            {
                throw new Exception("Workout category is in use and cannot be deleted!");
            }

            context.WorkoutCategories.Remove(exists);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
