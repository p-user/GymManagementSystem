﻿namespace WorkoutCatalog.Workouts.Features.Workout.UpdateWorkout
{
    public record UpdateWorkoutCommand(UpdateWorkoutDto dto) : IRequest<Results<Guid>>;
    public class UpdateworkoutCommandHandler(WorkoutCatalogDbContext workoutCatalogDbContext, IValidator<UpdateWorkoutCommand> _validator) : IRequestHandler<UpdateWorkoutCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
        {
            var entity = await workoutCatalogDbContext.Workouts.FindAsync(request.dto.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Workout not found");
            }

            await _validator.ValidateAsync(request, cancellationToken);
            entity.Update(request.dto.Name, request.dto.Description);
            await workoutCatalogDbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }


    }

}