
namespace WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroups
{
    public record GetMuscleGroupsQuery() : IRequest<Results<List<ViewMuscleGroupDto>>>;
    public class GetMuscleGroupsQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetMuscleGroupsQuery, Results<List<ViewMuscleGroupDto>>>
    {
        public async Task<Results<List<ViewMuscleGroupDto>>> Handle(GetMuscleGroupsQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await context.MuscleGroups.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewMuscleGroupDto>>(muscleGroups);
        }
    }
}
