

namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateExerciseDto
    {

        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? DescriptionLink { get; private set; }

        public IReadOnlyList<ViewMuscleGroupDto> MuscleGroups = new List<ViewMuscleGroupDto>();

        public Guid ExerciseCategory { get; private set; } 
    }


    public record ViewExerciseDto : CreateExerciseDto
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }

    public record UpdateExerciseDto
    {

        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? DescriptionLink { get; private set; }
        public Guid ExerciseCategory { get; private set; }
    }
}
