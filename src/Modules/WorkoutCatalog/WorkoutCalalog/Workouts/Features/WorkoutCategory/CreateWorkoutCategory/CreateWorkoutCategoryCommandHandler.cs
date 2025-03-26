namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.CreateWorkoutCategory
{
    public record CreateWorkoutCategoryCommand(CreateWorkoutCategoryDto dto) : IRequest<Results<Guid>>;
    public class CreateWorkoutCategoryCommandHandler(WorkoutCatalogDbContext context, IValidator<CreateWorkoutCategoryCommand> _validator) : IRequestHandler<CreateWorkoutCategoryCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(CreateWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var isValid = await context.WorkoutCategories.AnyAsync(w => w.Name.ToLower() == request.dto.Name.ToLower(), cancellationToken);
            if (isValid)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.WorkoutCategoryErrors.NameConflict(request.dto.Name));

            }
            var entity = CreateWorkoutCategory(request.dto);
            await context.WorkoutCategories.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        private Models.WorkoutCategory CreateWorkoutCategory(CreateWorkoutCategoryDto dto)
        {
            return Models.WorkoutCategory.Create(dto.Name, dto.Description);
        }
    }
}
