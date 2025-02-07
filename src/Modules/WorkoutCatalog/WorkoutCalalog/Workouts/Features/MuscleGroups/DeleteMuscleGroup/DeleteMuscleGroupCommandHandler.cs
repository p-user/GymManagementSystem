namespace WorkoutCatalog.Workouts.Features.MuscleGroups.DeleteMuscleGroup
{
    public record DeleteMuscleGroupCommand(Guid Id) : IRequest<bool>;
    public class DeleteMuscleGroupCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteMuscleGroupCommand, bool>
    {
        public async Task<bool> Handle(DeleteMuscleGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.MuscleGroups.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Muscle group not found");
            }

            var deleteCriteria = await context.Exercises.Where(e => e.MuscleGroups.Select(mg => mg.Id).Contains(entity.Id)).AnyAsync(cancellationToken);
            if (deleteCriteria)
            {
                throw new Exception("Muscle group is in use");
            }
            context.MuscleGroups.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
