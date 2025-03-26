namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.UpdateWorkoutCategory
{
    public record UpdateWorkoutCategoryCommand(UpdateWorkoutCategoryDto dto) : IRequest<Results<Guid>>;
    public class UpdateWorkoutCategoryCommandHandler(WorkoutCatalogDbContext context, IValidator<UpdateWorkoutCategoryCommand> _validator) : IRequestHandler<UpdateWorkoutCategoryCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(UpdateWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.WorkoutCategories.FirstOrDefaultAsync(s => s.Id == request.dto.Id, cancellationToken);
            if (entity == null)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.WorkoutCategoryErrors.NotFound(request.dto.Id));
            }

            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            entity.Update(request.dto.Name, request.dto.Description);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
