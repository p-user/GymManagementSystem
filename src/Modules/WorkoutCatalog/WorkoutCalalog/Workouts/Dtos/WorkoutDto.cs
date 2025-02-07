
namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateWorkoutDto
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public List<Guid> Exercises { get; private set; } = new();
        public List<Guid> Categories { get; private set; } = new();
    }

    public record UpdateWorkoutDto 
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
    }

    public record ViewWorkoutDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public List<ViewExerciseDto> Exercises { get; private set; } = new();
        public List<ViewWorkoutCategoryDto> WorkoutCategories { get; private set; } = new();
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
