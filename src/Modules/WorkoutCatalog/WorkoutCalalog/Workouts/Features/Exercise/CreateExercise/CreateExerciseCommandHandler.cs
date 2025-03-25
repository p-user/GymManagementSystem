namespace WorkoutCatalog.Workouts.Features.Exercise.CreateExercise
{
    public record CreateExerciseCommand(CreateExerciseDto dto) : IRequest<Results<Guid>>;
    public class CreateExerciseCommandHandler(WorkoutCatalogDbContext context, IValidator<CreateExerciseCommand> _validator) : IRequestHandler<CreateExerciseCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            //validate request
            await _validator.ValidateAsync(request, cancellationToken);

            //validate FKs and retrive muscle groups
            var muscleIds = request.dto.MuscleGroups.Select(mg => mg.Id).ToList();
            var muscleGroups = await context.MuscleGroups.Where(mg => muscleIds.Contains(mg.Id)).ToListAsync();
            if (!muscleGroups.Any())
            {
                throw new Exception("Muscle groups not found");//TODO: create custom exception
            }

            var category = await context.ExerciseCategories.Where(s => s.Id == request.dto.ExerciseCategory).AnyAsync(cancellationToken);
            if (!category)
            {
                throw new Exception("Exercise category not found");
            }

            // crete excercise
            var entity = CreateExcercise(request.dto, muscleGroups);

            //save to db
            await context.Exercises.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);


            return entity.Id;
        }


        private Models.Exercise CreateExcercise(CreateExerciseDto dto, List<MuscleGroup> muscleGroups)
        {
            return Models.Exercise.Create(dto.Name, dto.Description, dto.DescriptionLink, dto.ExerciseCategory, muscleGroups);
        }
    }
}
