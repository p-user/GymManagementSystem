

namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateExerciseDto
    {

        public string? Name { get; init ; }
        public string? Description { get; init; }
        public string? DescriptionLink { get; init; }

        public List<ViewMuscleGroupDto> MuscleGroups { get; init; } = new List<ViewMuscleGroupDto>();

        public Guid ExerciseCategory { get; init; } 
    }


    public record ViewExerciseDto : CreateExerciseDto
    {
        public Guid Id { get; init; }
        public DateTime? CreatedAt { get;   init; }
        public string? CreatedBy { get; init; }
        public string? LastModifiedBy { get; init; }
        public DateTime? LastModifiedAt { get; init; }
    }

    public record UpdateExerciseDto
    {

        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? DescriptionLink { get; init; }
        public Guid ExerciseCategory { get; init; }
    }
}
