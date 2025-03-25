namespace WorkoutCatalog.Workouts.Features.Exercise.GetExercises
{
    public record GetExercisesQuery() : IRequest<Results<List<ViewExerciseDto>>>;
    public class GetExercisesQueryHandler(WorkoutCatalogDbContext workoutCatalogDbContext, IMapper mapper) : IRequestHandler<GetExercisesQuery, Results<List<ViewExerciseDto>>>
    {
        public async Task<Results<List<ViewExerciseDto>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            var entities = await workoutCatalogDbContext.Exercises.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewExerciseDto>>(entities);
        }
    }
}
