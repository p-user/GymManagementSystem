

namespace WorkoutCatalog.Workouts.Dtos
{
    public record CreateMuscleGroupDto
    {
        public string Muscle { get; init; }
        public string? Description { get; init; }
    }

    public record ViewMuscleGroupDto : CreateMuscleGroupDto
    {
        public Guid Id { get; init; }
        public DateTime? CreatedAt { get; init; }
        public string? CreatedBy { get; init; }
        public string? LastModifiedBy { get; init; }
        public DateTime? LastModifiedAt { get; init; }
    }

    public record UpdateMuscleGroupDto : CreateMuscleGroupDto
    {
        public Guid Id { get; init; }
    }

}
