namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategories
{
    public record GetExerciseCategoriesQuery : IRequest<Results<List<ViewExerciseCategoryDto>>>;
    public class GetExerciseCategoriesQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetExerciseCategoriesQuery, Results<List<ViewExerciseCategoryDto>>>
    {
        public async Task<Results<List<ViewExerciseCategoryDto>>> Handle(GetExerciseCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await context.ExerciseCategories.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewExerciseCategoryDto>>(entities);
        }
    }

}
