namespace WorkoutCatalog.Models
{
    public class ExerciseCategory : Entity<Guid>
    {
        //compound, isolation, bodyweight, etc

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public List<Exercise> Exercises { get; private set; } = new();

        public static ExerciseCategory Create(string name, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            return new ExerciseCategory
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
        }

        public void Update(string name, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            Name = name;
            Description = description;
        }

    }
}
