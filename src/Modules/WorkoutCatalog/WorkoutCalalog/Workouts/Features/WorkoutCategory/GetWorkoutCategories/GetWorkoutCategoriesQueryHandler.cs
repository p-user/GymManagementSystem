namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategories
{
    public record GetWorkoutCategoriesQuery() : IRequest<Results<List<ViewWorkoutCategoryDto>>>;
    public class GetWorkoutCategoriesQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetWorkoutCategoriesQuery, Results<List<ViewWorkoutCategoryDto>>>
    {
        public async Task<Results<List<ViewWorkoutCategoryDto>>> Handle(GetWorkoutCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await context.WorkoutCategories.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewWorkoutCategoryDto>>(entities);

        }
    }
}
