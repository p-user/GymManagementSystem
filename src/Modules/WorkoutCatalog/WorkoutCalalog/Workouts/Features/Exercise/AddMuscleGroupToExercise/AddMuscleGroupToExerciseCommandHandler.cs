namespace WorkoutCatalog.Workouts.Features.Exercise.AddMuscleGroupToExercise
{
    public record AddMuscleGroupToExerciseCommand(Guid exerciseId, Guid muscleGroup) : IRequest<Results<ViewExerciseDto>>;

    public class AddMuscleGroupToExerciseCommandHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<AddMuscleGroupToExerciseCommand, Results<ViewExerciseDto>>
    {
        public async Task<Results<ViewExerciseDto>> Handle(AddMuscleGroupToExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = await context.Exercises.Include(e => e.MuscleGroups).FirstOrDefaultAsync(e => e.Id == request.exerciseId, cancellationToken);
            if (exercise is null)
            {
                return (Results<ViewExerciseDto>)Shared.Results.Results.Failure(ModuleErrors.ExerciseErrors.NotFound(request.exerciseId));
            }

            var muscleGroup = await context.MuscleGroups.FirstOrDefaultAsync(mg => mg.Id == request.muscleGroup, cancellationToken);

            if (muscleGroup is null)
            {
                return (Results<ViewExerciseDto>)Shared.Results.Results.Failure(ModuleErrors.MuscleGroupError.NotFound(request.muscleGroup));
            }

            exercise.AddMuscleGroup(muscleGroup);
            context.Exercises.Update(exercise);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<ViewExerciseDto>(exercise);
        }
    }
}
