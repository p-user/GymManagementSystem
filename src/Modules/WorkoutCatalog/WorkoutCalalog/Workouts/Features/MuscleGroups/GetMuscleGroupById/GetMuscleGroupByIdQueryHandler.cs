namespace WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroupById
{
    public record GetMuscleGroupByIdQuery(Guid Id) : IRequest<Results<ViewMuscleGroupDto>>;
    public class GetMuscleGroupByIdQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetMuscleGroupByIdQuery, Results<ViewMuscleGroupDto>>
    {
        public async Task<Results<ViewMuscleGroupDto>> Handle(GetMuscleGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.MuscleGroups.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Muscle group not found");
            }

            return mapper.Map<ViewMuscleGroupDto>(entity);
        }
    }
}
