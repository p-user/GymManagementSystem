namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.CreateExerciseCategory
{
    public record CreateExerciseCategoryCommand(CreateExerciseCategoryDto dto) : IRequest<Results<Guid>>;
    public class CreateExerciseCategoryCommandHandler(WorkoutCatalogDbContext context, IValidator<CreateExerciseCategoryCommand> _validator) : IRequestHandler<CreateExerciseCategoryCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(CreateExerciseCategoryCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var entity = CreateExerciseCategory(request.dto);
            await context.ExerciseCategories.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        private Models.ExerciseCategory CreateExerciseCategory(CreateExerciseCategoryDto dto)
        {
            return Models.ExerciseCategory.Create(dto.Name, dto.Description);
        }
    }
}
