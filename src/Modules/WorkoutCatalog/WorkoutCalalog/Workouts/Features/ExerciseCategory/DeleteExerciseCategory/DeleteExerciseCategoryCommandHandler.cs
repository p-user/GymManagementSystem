namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.DeleteExerciseCategory
{
    public record DeleteExcerciseCategoryCommand(Guid Id) : IRequest<Shared.Results.Results>;
    public class DeleteExerciseCategoryCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteExcerciseCategoryCommand, Shared.Results.Results>
    {
        public async Task<Shared.Results.Results> Handle(DeleteExcerciseCategoryCommand request, CancellationToken cancellationToken)
        {
            var existing = await context.ExerciseCategories.FindAsync(request.Id);
            if (existing == null) { throw new Exception("Excercise category was not found!"); }

            var isDeletable = await context.ExerciseCategories.Include(s => s.Exercises).AnyAsync(e => e.Exercises.Count > 0, cancellationToken);
            if (isDeletable) { throw new Exception("Excercise category is in use!"); }

            context.ExerciseCategories.Remove(existing);
            await context.SaveChangesAsync(cancellationToken);
            return Shared.Results.Results.Success();
        }
    }
}
