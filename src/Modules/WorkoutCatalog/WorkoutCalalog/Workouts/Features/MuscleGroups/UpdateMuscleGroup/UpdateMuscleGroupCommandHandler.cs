
namespace WorkoutCatalog.Workouts.Features.MuscleGroups.UpdateMuscleGroup
{

    public record UpdateMuscleGroupCommand(Guid Id, CreateMuscleGroupDto CreateMuscleGroupDto) : IRequest<Results<Guid>>;
    public class UpdateMuscleGroupCommandHandler(WorkoutCatalogDbContext context, IValidator<UpdateMuscleGroupCommand> _validator) : IRequestHandler<UpdateMuscleGroupCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(UpdateMuscleGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.MuscleGroups.FindAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                throw new Exception("Muscle group not found");
            }

            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            entity.Update(request.CreateMuscleGroupDto.Muscle, request.CreateMuscleGroupDto.Description);
            context.MuscleGroups.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
