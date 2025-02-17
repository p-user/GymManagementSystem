
namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateWorkoutDto
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<Guid> Exercises { get; init; } = new();
        public List<Guid> Categories { get; init; } = new();
    }

    public record UpdateWorkoutDto 
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string? Description { get;  set; }
    }

    public record ViewWorkoutDto
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public List<ViewExerciseDto> Exercises { get;  set; } = new();
        public List<ViewWorkoutCategoryDto> WorkoutCategories { get;  set; } = new();
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
