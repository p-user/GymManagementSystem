
using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategories
{
    public record GetExerciseCategoriesQuery : IRequest<List<ViewExerciseCategoryDto>>;
    public class GetExerciseCategoriesQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetExerciseCategoriesQuery, List<ViewExerciseCategoryDto>>
    {
        public async Task<List<ViewExerciseCategoryDto>> Handle(GetExerciseCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await context.ExerciseCategories.ToListAsync(cancellationToken);
            return mapper.Map<List<ViewExerciseCategoryDto>>(entities);
        }
    }

}
