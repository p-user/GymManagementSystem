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

            var deleteCriteria = await context.MuscleGroups.Include(s => s.Exercises).AnyAsync(s=>s.Exercises.Count>0, cancellationToken);
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
