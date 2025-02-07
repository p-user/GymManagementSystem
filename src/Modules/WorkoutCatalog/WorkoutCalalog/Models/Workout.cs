namespace WorkoutCatalog.Models
{
    public class Workout : Entity<Guid>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        private List<Exercise> _exercises = new();
        public IReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();
        private List<WorkoutCategory> _workoutCategories = new();
        public IReadOnlyCollection<WorkoutCategory> WorkoutCategories => _workoutCategories.AsReadOnly();



        public static Workout Create(string name, string? description, List<Exercise> exercises)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

            if (exercises.Count == 0)
            {
                throw new InvalidOperationException("Workout must have at least one exercise");
            }
            return new Workout
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                _exercises = exercises
            };
        }
    }
}
