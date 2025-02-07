namespace WorkoutCatalog.Workouts.Features.MuscleGroups.UpdateMuscleGroup
{

    public record UpdateMuscleGroupCommand(Guid Id, CreateMuscleGroupDto CreateMuscleGroupDto) : IRequest<Guid>;
    public class UpdateMuscleGroupCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<UpdateMuscleGroupCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateMuscleGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.MuscleGroups.FindAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                throw new Exception("Muscle group not found");
            }

            entity.Update(request.CreateMuscleGroupDto.Muscle, request.CreateMuscleGroupDto.Description);
            context.MuscleGroups.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
