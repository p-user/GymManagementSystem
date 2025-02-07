namespace WorkoutCatalog.Workouts.Features.Exercise.CreateExcercise
{
    public record CreateExcerciseCommand(CreateExerciseDto dto) : IRequest<bool>;
    public class CreateExcerciseCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<CreateExcerciseCommand, bool>
    {
        public async Task<bool> Handle(CreateExcerciseCommand request, CancellationToken cancellationToken)
        {
            //validate FKs and retrive muscle groups

            var muscleGroups = await context.MuscleGroups.Where(mg => request.dto.MuscleGroups.Select(mg => mg.Id).Contains(mg.Id)).ToListAsync();
            if (!muscleGroups.Any())
            {
                throw new Exception("Muscle groups not found");
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


            return true;
        }


        private Models.Exercise CreateExcercise(CreateExerciseDto dto, List<MuscleGroup> muscleGroups)
        {
            return Models.Exercise.Create(dto.Name, dto.Description, dto.DescriptionLink, dto.ExerciseCategory, muscleGroups);
        }
    }
}
