namespace WorkoutCatalog.Workouts.Features.WorkoutCategory.GetWorkoutCategoryById
{
    public record GetWorkoutCategoryByIdQuery(Guid Id) : IRequest<Results<ViewWorkoutCategoryDto>>;
    public class GetWorkoutCategoryByIdQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetWorkoutCategoryByIdQuery, Results<ViewWorkoutCategoryDto>>
    {
        public async Task<Results<ViewWorkoutCategoryDto>> Handle(GetWorkoutCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.WorkoutCategories.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return (Results<ViewWorkoutCategoryDto>)Shared.Results.Results.Failure(ModuleErrors.WorkoutCategoryErrors.NotFound(request.Id));
            }
            return mapper.Map<ViewWorkoutCategoryDto>(entity);

        }
    }
}
