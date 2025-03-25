

namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateWorkoutCategoryDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public record ViewWorkoutCategoryDto : CreateWorkoutCategoryDto
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }

    public record UpdateWorkoutCategoryDto : CreateWorkoutCategoryDto
    {
        public Guid Id { get; set; }
    }
}
