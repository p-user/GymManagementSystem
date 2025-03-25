

namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateExerciseCategoryDto
    {
        public string Name { get; init; }
        public string? Description { get; init; }
    }

    public record ViewExerciseCategoryDto : CreateExerciseCategoryDto
    {
        public Guid Id { get; init; }
        public DateTime? CreatedAt { get; init; }
        public string? CreatedBy { get; init; }
        public string? LastModifiedBy { get; init; }
        public DateTime? LastModifiedAt { get; init; }
    }
}
