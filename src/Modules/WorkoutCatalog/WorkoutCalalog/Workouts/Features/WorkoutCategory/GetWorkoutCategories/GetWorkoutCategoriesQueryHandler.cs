

using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategories
{
    public record GetWorkoutCategoriesQuery() : IRequest<List<ViewWorkoutCategoryDto>>;
    public class GetWorkoutCategoriesQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetWorkoutCategoriesQuery, List<ViewWorkoutCategoryDto>>
    {
        public async Task<List<ViewWorkoutCategoryDto>> Handle(GetWorkoutCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await context.WorkoutCategories.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewWorkoutCategoryDto>>(entities);
        }
    }
}
