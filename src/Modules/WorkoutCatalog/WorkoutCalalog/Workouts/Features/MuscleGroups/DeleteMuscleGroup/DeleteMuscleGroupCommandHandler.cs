﻿
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
                return Shared.Results.Results.Failure(ModuleErrors.MuscleGroupError.NotFound(request.Id));
            }

            var deleteCriteria = await context.MuscleGroups.Include(s => s.Exercises).AnyAsync(s => s.Exercises.Count > 0, cancellationToken);
            if (deleteCriteria)
            {
                return Shared.Results.Results.Failure(ModuleErrors.MuscleGroupError.DeleteConflict());
            }
            context.MuscleGroups.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Success();
        }
    }

}
