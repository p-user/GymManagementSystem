
using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.Exercise.GetExercises
{
    public record GetExercisesQuery() : IRequest<List<ViewExerciseDto>>;
    public class GetExercisesQueryHandler(WorkoutCatalogDbContext workoutCatalogDbContext, IMapper mapper) : IRequestHandler<GetExercisesQuery, List<ViewExerciseDto>>
    {
        public async Task<List<ViewExerciseDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            var entities = await workoutCatalogDbContext.Exercises.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewExerciseDto>>(entities);
        }
    }
}
