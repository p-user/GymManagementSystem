
using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.MuscleGroups.GetMuscleGroupById
{
    public record GetMuscleGroupByIdQuery(Guid Id) : IRequest<ViewMuscleGroupDto>;
    public class GetMuscleGroupByIdQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetMuscleGroupByIdQuery, ViewMuscleGroupDto>
    {
        public async Task<ViewMuscleGroupDto> Handle(GetMuscleGroupByIdQuery request, CancellationToken cancellationToken)
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
