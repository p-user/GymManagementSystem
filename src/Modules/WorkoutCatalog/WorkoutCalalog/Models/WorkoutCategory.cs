namespace WorkoutCatalog.Models
{
    public class WorkoutCategory : Entity<Guid>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }

        private List<Workout> _workouts = new();
        public IReadOnlyList<Workout> Workouts => _workouts.AsReadOnly();

        public static WorkoutCategory Create(string name, string? description)
        {

            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            return new WorkoutCategory
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
