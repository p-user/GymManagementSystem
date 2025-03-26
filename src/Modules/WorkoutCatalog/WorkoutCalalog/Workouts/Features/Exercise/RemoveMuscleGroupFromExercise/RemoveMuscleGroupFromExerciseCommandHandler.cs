
using Results = Shared.Results.Results;

namespace WorkoutCatalog.Workouts.Features.Exercise.RemoceMuscleGroupFromExercise
{

    public record RemoveMuscleGroupFromExerciseCommand(Guid exerciseId, Guid muscleGroupId) : IRequest<Results>;
    public class RemoveMuscleGroupFromExerciseCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<RemoveMuscleGroupFromExerciseCommand, Results>
    {
        public async Task<Results> Handle(RemoveMuscleGroupFromExerciseCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Exercises.Include(e => e.MuscleGroups).FirstOrDefaultAsync(e => e.Id == request.exerciseId, cancellationToken);

            if (entity is null)
            {
                return Shared.Results.Results.Failure(ModuleErrors.ExerciseErrors.NotFound(request.exerciseId));
            }

            entity.RemoveMuscleGroup(request.muscleGroupId);

            await context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }
}
