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



        public static Workout Create(string name, string? description, List<Exercise> exercises, List<WorkoutCategory> categories)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));

            if (exercises.Count == 0)
            {
                throw new InvalidOperationException("Workout must have at least one exercise");
            }


            if (categories.Count == 0)
            {
                throw new InvalidOperationException("Workout must have at least one category for filtering");
            }

            return new Workout
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                _exercises = exercises,
                _workoutCategories = categories
            };
        }


        public void Update(string name, string? description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));



            Name = name;
            Description = description;
               
        }

        public void AddWorkoutCategories(List<WorkoutCategory> categories)
        {
            if(categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            foreach (var category in categories)
            {
                if (!_workoutCategories.Contains(category))
                {
                    _workoutCategories.Add(category);
                }
                else
                {
                    throw new InvalidOperationException("Category already added");
                }
            }
        }


        public void RemoveWorkoutCategory(WorkoutCategory category)
        {
           
                if (_workoutCategories.Contains(category))
                {
                    _workoutCategories.Remove(category);
                }
                else
                {
                    throw new InvalidOperationException("This category has not been set to this workout");
                }
            
        }
    }
}
