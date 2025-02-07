

using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroups
{
    public record GetMuscleGroupsQuery() : IRequest<List<ViewMuscleGroupDto>>;
    public class GetMuscleGroupsQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetMuscleGroupsQuery, List<ViewMuscleGroupDto>>
    {
        public async Task<List<ViewMuscleGroupDto>> Handle(GetMuscleGroupsQuery request, CancellationToken cancellationToken)
        {
            var muscleGroups = await context.MuscleGroups.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewMuscleGroupDto>>(muscleGroups);
        }
    }
}
