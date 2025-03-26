namespace WorkoutCatalog.Workouts.Features.MuscleGroups.CreateMuscleGroup
{
    public record CreateMuscleGroupCommand(CreateMuscleGroupDto dto) : IRequest<Results<Guid>>;
    public class CreateMuscleGroupCommandHandler(WorkoutCatalogDbContext context, IValidator<CreateMuscleGroupCommand> validator) : IRequestHandler<CreateMuscleGroupCommand, Results<Guid>>
    {
        public async Task<Results<Guid>> Handle(CreateMuscleGroupCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            //validate name
            var isValid = await context.MuscleGroups.AnyAsync(mg => mg.Muscle.ToLower() == request.dto.Muscle.ToLower(), cancellationToken);
            if (isValid)
            {
                return (Results<Guid>)Shared.Results.Results.Failure(ModuleErrors.MuscleGroupError.NameConflict(request.dto.Muscle));
            }

            // crete muscle group
            var entity = CreateMuscleGroup(request.dto);

            //save to db
            await context.MuscleGroups.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }



        private MuscleGroup CreateMuscleGroup(CreateMuscleGroupDto dto)
        {
            return MuscleGroup.Create(dto.Muscle, dto.Description);
        }
    }
}