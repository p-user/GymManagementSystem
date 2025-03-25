namespace WorkoutCatalog.Workouts.Features.Exercise.GetExcerciseById
{

    public record GetExerciseByIdQuery(Guid Id) : IRequest<Results<ViewExerciseDto>>;
    public class GetExerciseByIdQueryHandler(WorkoutCatalogDbContext context, IMapper mapper) : IRequestHandler<GetExerciseByIdQuery, Results<ViewExerciseDto>>
    {
        public async Task<Results<ViewExerciseDto>> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Exercises
                .Include(e => e.MuscleGroups)
                .Include(e => e.ExerciseCategory)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new Exception("Exercise not found");
            }

            return MapToDto(entity);

        }

        private ViewExerciseDto MapToDto(Models.Exercise entity)
        {
            return mapper.Map<ViewExerciseDto>(entity);
        }
    }
}
