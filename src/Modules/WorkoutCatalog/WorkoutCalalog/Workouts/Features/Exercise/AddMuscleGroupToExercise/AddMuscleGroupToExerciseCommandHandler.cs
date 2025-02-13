
using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.Exercise.AddMuscleGroupToExercise
{
    public record  AddMuscleGroupToExerciseCommand (Guid exerciseId, Guid muscleGroup) : IRequest <ViewExerciseDto>;

    public class AddMuscleGroupToExerciseCommandHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<AddMuscleGroupToExerciseCommand, ViewExerciseDto>
    {
        public async Task<ViewExerciseDto> Handle(AddMuscleGroupToExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = await context.Exercises.Include(e => e.MuscleGroups).FirstOrDefaultAsync(e => e.Id == request.exerciseId, cancellationToken);
            if (exercise is null)
            {
                throw new Exception("Exercise was not found!");
            }

            var muscleGroup = await context.MuscleGroups.FirstOrDefaultAsync(mg => mg.Id == request.muscleGroup, cancellationToken);

            if (muscleGroup is null)
            {
                throw new Exception("Muscle group was not found!");
            }

            exercise.AddMuscleGroup(muscleGroup);
            context.Exercises.Update(exercise);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<ViewExerciseDto>(exercise);
        }
    }
}
