namespace WorkoutCatalog.Workouts.Features.Workout.CreateWorkout
{

    public record CreateWorkoutCommand(CreateWorkoutDto dto) : IRequest<Guid>;

    public class CreateWorkoutCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<CreateWorkoutCommand, Guid>
    {
        public async Task<Guid> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
        {
            //validate name
            var isValid = await context.Workouts.AnyAsync(w => w.Name.ToLower() == request.dto.Name.ToLower(), cancellationToken);
            if (isValid)
            {
                throw new Exception("Workout with same name  already exists!");
            }

            var exercises = await context.Exercises.Where(e => request.dto.Exercises.Contains(e.Id)).ToListAsync(cancellationToken);
            if (!exercises.Any())
            {
                throw new Exception("Workout must have at least one valid exercise!");
            }


            // create workout
            var entity = CreateWorkout(request.dto, exercises);
            //save to db
            await context.Workouts.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        private Models.Workout CreateWorkout(CreateWorkoutDto dto, List<Models.Exercise> exercises)
        {
            return Models.Workout.Create(dto.Name, dto.Description, exercises);
        }
    }

}
