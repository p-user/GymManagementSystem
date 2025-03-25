
using Results = Shared.Results.Results;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.DeleteMuscleGroup
{
    public record DeleteMuscleGroupCommand(Guid Id) : IRequest<Results>;
    public class DeleteMuscleGroupCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<DeleteMuscleGroupCommand, Results>
    {
        public async Task<Results> Handle(DeleteMuscleGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.MuscleGroups.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Muscle group not found");
            }

            var deleteCriteria = await context.MuscleGroups.Include(s => s.Exercises).AnyAsync(s => s.Exercises.Count > 0, cancellationToken);
            if (deleteCriteria)
            {
                throw new Exception("Muscle group is in use");
            }
            context.MuscleGroups.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }

}
