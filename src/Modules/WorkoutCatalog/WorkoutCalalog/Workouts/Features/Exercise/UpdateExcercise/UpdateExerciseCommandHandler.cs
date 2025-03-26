namespace WorkoutCatalog.Workouts.Features.Exercise.UpdateExcercise
{
    public record UpdateExerciseCommand(UpdateExerciseDto Dto, Guid Id) : IRequest<Results<Guid>>;


    public class UpdateExerciseCommandHandler(WorkoutCatalogDbContext context, IValidator<UpdateExerciseCommand> validator) : IRequestHandler<UpdateExerciseCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Exercises.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.ExerciseErrors.NotFound(request.Id));
            }

            var isValid = await context.ExerciseCategories.AnyAsync(e => e.Id == request.Dto.ExerciseCategory, cancellationToken);
            if (!isValid)
            {
                throw new Exception("Exercise category not found");
            }

            entity.Update(request.Dto.Name, request.Dto.Description, request.Dto.DescriptionLink, request.Dto.ExerciseCategory);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
