namespace WorkoutCatalog.Workouts.Features.Exercise.DeleteExercise
{
    public record DeleteExerciseCommand(Guid Id) : IRequest<bool>;
    public class DeleteExerciseCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteExerciseCommand, bool>
    {
        public async Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
           
            var entity = await context.Exercises.Include(s=>s.Workouts).FirstOrDefaultAsync(s=>s.Id == request.Id);
            if (entity == null)
            {
                throw new Exception("Exercise not found");
            }

          
            if (entity.Workouts.Count()>0)
            {
                throw new Exception("Exercise belongs to a workout routine, pleasae remove it from the routine");
            }

            context.Exercises.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
