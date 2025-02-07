namespace WorkoutCatalog.Workouts.Features.Exercise.DeleteExcercise
{
    public record DeleteExerciseCommand(Guid Id) : IRequest<bool>;
    public class DeleteExerciseCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteExerciseCommand, bool>
    {
        public async Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Exercises.FindAsync(request.Id);
            if (entity == null)
            {
                throw new Exception("Exercise not found");
            }

            var isDeletable = await context.Workouts.AnyAsync(e => e.Id == request.Id, cancellationToken);
            if (isDeletable)
            {
                throw new Exception("Exercise belongs to a workout routine, pleasae remove it from the routine");
            }

            context.Exercises.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
