namespace WorkoutCatalog.Workouts.Features.ExerciseCategory.GetExerciseCategoryById
{
    public record GetExerciseCategoryByIdQuery(Guid Id) : IRequest<Results<ViewExerciseCategoryDto>>;
    public class GetExerciseCategoryByIdQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetExerciseCategoryByIdQuery, Results<ViewExerciseCategoryDto>>
    {
        public async Task<Results<ViewExerciseCategoryDto>> Handle(GetExerciseCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.ExerciseCategories.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return (Results<ViewExerciseCategoryDto>)Shared.Results.Results.Failure(ModuleErrors.ExerciseCategoryErrors.NotFound(request.Id));
            }
            return mapper.Map<ViewExerciseCategoryDto>(entity);
        }
    }

}
