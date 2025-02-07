namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.DeleteExerciseCategory
{
    public record DeleteExcerciseCategoryCommand(Guid Id) : IRequest<bool>;
    public class DeleteExcerciseCategoryCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteExcerciseCategoryCommand, bool>
    {
        public async Task<bool> Handle(DeleteExcerciseCategoryCommand request, CancellationToken cancellationToken)
        {
            var existing = await context.ExerciseCategories.FindAsync(request.Id);
            if (existing == null) { throw new Exception("Excercise category was not found!"); }

            var isDeletable = await context.Exercises.AnyAsync(e => e.ExerciseCategory == request.Id, cancellationToken);
            if (isDeletable) { throw new Exception("Excercise category is in use!"); }

            context.ExerciseCategories.Remove(existing);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
