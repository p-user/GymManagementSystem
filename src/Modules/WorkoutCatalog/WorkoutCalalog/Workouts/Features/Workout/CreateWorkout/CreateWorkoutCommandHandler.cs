using Results = Shared.Results.Results;

namespace WorkoutCatalog.Workouts.Features.Workout.CreateWorkout
{

    public record CreateWorkoutCommand(CreateWorkoutDto dto) : IRequest<Results<Guid>>;

    public class CreateWorkoutCommandHandler(WorkoutCatalogDbContext context, IValidator<CreateWorkoutCommand> validator) : IRequestHandler<CreateWorkoutCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            //validate name
            var isValid = await context.Workouts.AnyAsync(w => w.Name.ToLower() == request.dto.Name.ToLower(), cancellationToken);
            if (isValid)
            {
                return (Results<Guid>)Results.Failure(ModuleErrors.WorkoutErrors.NameConflict(request.dto.Name));

            }

            var categories = await context.WorkoutCategories.Where(w => request.dto.Categories.Contains(w.Id)).ToListAsync(cancellationToken);
            if (!categories.Any())
            {
                return (Results<Guid>)Results.Failure(ModuleErrors.WorkoutErrors.InvalidExercises());
            }

            var exercises = await context.Exercises.Where(e => request.dto.Exercises.Contains(e.Id)).ToListAsync(cancellationToken);
            if (!exercises.Any())
            {
                return (Results<Guid>)Results.Failure(ModuleErrors.WorkoutErrors.InvalidExercises());
            }


            // create workout
            var entity = CreateWorkout(request.dto, exercises, categories);
            //save to db
            await context.Workouts.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        private Models.Workout CreateWorkout(CreateWorkoutDto dto, List<Models.Exercise> exercises, List<Models.WorkoutCategory> workoutCategories)
        {
            return Models.Workout.Create(dto.Name, dto.Description, exercises, workoutCategories);
        }
    }

}
