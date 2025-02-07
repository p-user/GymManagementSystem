namespace WorkoutCatalog.Workouts.Features.MuscleGroups.CreateMuscleGroup
{
    public record CreateMuscleGroupCommand(CreateMuscleGroupDto dto) : IRequest<Guid>;
    public class CreateMuscleGroupCommandHandler(WorkoutCatalogDbContext context) : IRequestHandler<CreateMuscleGroupCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMuscleGroupCommand request, CancellationToken cancellationToken)
        {
            //validate name
            var isValid = await context.MuscleGroups.AnyAsync(mg => mg.Muscle.ToLower() == request.dto.Muscle.ToLower(), cancellationToken);
            if (isValid)
            {
                throw new Exception("Muscle group already exists");
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