using Results = Shared.Results.Results;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.UpdateExerciseCategory
{

    public record UpdateExerciseCategoryCommand(CreateExerciseCategoryDto dto, Guid Id) : IRequest<Results>;
    public class UpdateExerciseCategoryCommandHandler(WorkoutCatalogDbContext context, IValidator<UpdateExerciseCategoryCommand> _validator) : IRequestHandler<UpdateExerciseCategoryCommand, Results>
    {
        public async Task<Results> Handle(UpdateExerciseCategoryCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var entity = await context.ExerciseCategories.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Exercise category not found");
            }
            entity.Update(request.dto.Name, request.dto.Description);
            context.ExerciseCategories.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }

}
