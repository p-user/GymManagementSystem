namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.UpdateExerciseCategory
{

    public record UpdateExerciseCategoryCommand(CreateExerciseCategoryDto dto, Guid Id) : IRequest<bool>;
    public class UpdateExerciseCategoryCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<UpdateExerciseCategoryCommand, bool>
    {
        public async Task<bool> Handle(UpdateExerciseCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.ExerciseCategories.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Exercise category not found");
            }
            entity.Update(request.dto.Name, request.dto.Description);
            context.ExerciseCategories.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
