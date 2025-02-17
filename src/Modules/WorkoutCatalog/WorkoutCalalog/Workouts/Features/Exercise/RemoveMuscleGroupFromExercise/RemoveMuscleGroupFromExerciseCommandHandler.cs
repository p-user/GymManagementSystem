

using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.Exercise.RemoceMuscleGroupFromExercise
{

    public record RemoveMuscleGroupFromExerciseCommand(Guid exerciseId, Guid muscleGroupId) : IRequest<bool>;
    public class RemoveMuscleGroupFromExerciseCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<RemoveMuscleGroupFromExerciseCommand, bool>
    {
        public async  Task<bool> Handle(RemoveMuscleGroupFromExerciseCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Exercises.Include(e => e.MuscleGroups).FirstOrDefaultAsync(e => e.Id == request.exerciseId, cancellationToken);

            if (entity is null)
            {
                throw new Exception("Exercise was not found!");
            }

           entity.RemoveMuscleGroup(request.muscleGroupId);

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
