namespace WorkoutCatalog.Workouts.Features.Exercise.DeleteExercise
{
    public record DeleteExerciseCommand(Guid Id) : IRequest<Shared.Results.Results>;
    public class DeleteExerciseCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteExerciseCommand, Shared.Results.Results>
    {
        public async Task<Shared.Results.Results> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {

            var entity = await context.Exercises.Include(s => s.Workouts).FirstOrDefaultAsync(s => s.Id == request.Id);
            if (entity == null)
            {
                throw new Exception("Exercise not found");
            }


            if (entity.Workouts.Count() > 0)
            {
                throw new Exception("Exercise belongs to a workout routine, pleasae remove it from the routine");
            }

            context.Exercises.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Shared.Results.Results.Success();
        }
    }
}
