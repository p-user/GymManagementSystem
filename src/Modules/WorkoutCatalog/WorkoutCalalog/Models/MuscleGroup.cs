namespace WorkoutCatalog.Models
{
    public class MuscleGroup : Entity<Guid>
    {

        public string Muscle { get; private set; }
        public string? Description { get; private set; }

        public static MuscleGroup Create(string muscle, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(muscle, nameof(muscle));
            return new MuscleGroup
            {
                Id = Guid.NewGuid(),
                Muscle = muscle,
                Description = description
            };
        }

        public void Update(string muscle, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(muscle, nameof(muscle));
            Muscle = muscle;
            Description = description;

        }
    }
}
