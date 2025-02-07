namespace WorkoutCatalog.Models
{
    public class ExerciseCategory : Entity<Guid>
    {
        //compound, isolation, bodyweight, etc

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public List<Exercise> Exercises { get; private set; } = new();

        public static ExerciseCategory Create(string Name, string? Description)
        {
            ArgumentException.ThrowIfNullOrEmpty(Name, nameof(Name));

            return new ExerciseCategory
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Description = Description
            };
        }

        public void Update(string Name, string? Description)
        {
            ArgumentException.ThrowIfNullOrEmpty(Name, nameof(Name));

            Name = Name;
            Description = Description;
        }

    }
}
