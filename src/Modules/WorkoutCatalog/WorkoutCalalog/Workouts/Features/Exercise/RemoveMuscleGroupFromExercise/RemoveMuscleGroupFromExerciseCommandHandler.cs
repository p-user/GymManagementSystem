

using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.Exercise.RemoceMuscleGroupFromExercise
{

    public record RemoveMuscleGroupFromExerciseCommand(Guid exerciseId, Guid muscleGroupId) : IRequest<ViewExerciseDto>;
    public class RemoveMuscleGroupFromExerciseCommandHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<RemoveMuscleGroupFromExerciseCommand, ViewExerciseDto>
    {
        public async  Task<ViewExerciseDto> Handle(RemoveMuscleGroupFromExerciseCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Exercises.Include(e => e.MuscleGroups).FirstOrDefaultAsync(e => e.Id == request.exerciseId, cancellationToken);

            if (entity is null)
            {
                throw new Exception("Exercise was not found!");
            }

           entity.RemoveMuscleGroup(request.muscleGroupId);

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<ViewExerciseDto>(entity);
        }
    }
}
