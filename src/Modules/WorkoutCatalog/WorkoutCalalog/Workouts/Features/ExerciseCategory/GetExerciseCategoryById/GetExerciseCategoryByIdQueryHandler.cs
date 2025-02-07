

using AutoMapper;

namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategoryById
{
    public record GetExerciseCategoryByIdQuery(Guid Id) : IRequest<ViewExerciseCategoryDto>;
    public class GetExerciseCategoryByIdQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetExerciseCategoryByIdQuery, ViewExerciseCategoryDto>
    {
        public async Task<ViewExerciseCategoryDto> Handle(GetExerciseCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.ExerciseCategories.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Exercise category not found");
            }
            return mapper.Map<ViewExerciseCategoryDto>(entity);
        }
    }

}
