namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.DeleteWorkoutCategory
{
    public record DeleteWorkoutCategoryCommand(Guid Id) : IRequest<Shared.Results.Results>;
    public class DeleteWorkoutCategoryCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteWorkoutCategoryCommand, Shared.Results.Results>
    {
        public async Task<Shared.Results.Results> Handle(DeleteWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            var exists = await context.WorkoutCategories.FindAsync(request.Id, cancellationToken);
            if (exists == null)
            {
                return Shared.Results.Results.Failure(ModuleErrors.WorkoutCategoryErrors.NotFound(request.Id));
            }

            var isDeletable = await context.WorkoutCategories.Include(w => w.Workouts)
                .AnyAsync(w => w.Id == request.Id && w.Workouts.Count > 0, cancellationToken);
            if (isDeletable)
            {
                return Shared.Results.Results.Failure(ModuleErrors.WorkoutCategoryErrors.DeleteConflict());
            }

            context.WorkoutCategories.Remove(exists);
            await context.SaveChangesAsync(cancellationToken);

            return Shared.Results.Results.Success();
        }
    }
}
