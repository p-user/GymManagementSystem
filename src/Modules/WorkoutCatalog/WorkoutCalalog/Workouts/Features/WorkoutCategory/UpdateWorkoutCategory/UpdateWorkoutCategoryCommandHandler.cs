namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.UpdateWorkoutCategory
{
    public record UpdateWorkoutCategoryCommand(UpdateWorkoutCategoryDto dto) : IRequest<Guid>;
    public class UpdateWorkoutCategoryCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<UpdateWorkoutCategoryCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateWorkoutCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.WorkoutCategories.FirstOrDefaultAsync(s=>s.Id==request.dto.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Workout category not found!");
            }
            entity.Update(request.dto.Name, request.dto.Description);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
