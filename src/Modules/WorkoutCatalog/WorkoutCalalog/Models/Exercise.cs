
namespace WorkoutCatalog.Models
{
    public class Exercise : Aggregation<Guid> //  should dispatch event to update the exercise in the workouts , when the exercise is updated   
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? DescriptionLink { get; private set; }
        private List<MuscleGroup> muscleGroups { get; set; } = new();
        public IReadOnlyList<MuscleGroup> MuscleGroups => muscleGroups.AsReadOnly();

        public Guid ExerciseCategory { get; private set; } //fk to exercise category



        public static Exercise Create(string name, string? description, string? descriptionLink, Guid exerciseCategory, List<MuscleGroup> muscleGroups)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(descriptionLink, nameof(descriptionLink));
            return new Exercise
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                DescriptionLink = descriptionLink,
                muscleGroups = muscleGroups,
                ExerciseCategory = exerciseCategory
            };
        }

        public void Update(string name, string? description, string? descriptionLink, Guid exerciseCategory, List<MuscleGroup> muscleGroups)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(descriptionLink, nameof(descriptionLink));
            Name = name;
            Description = description;
            DescriptionLink = descriptionLink;
            muscleGroups = muscleGroups;
            ExerciseCategory = exerciseCategory;
        }

        public void AddMuscleGroup(MuscleGroup muscleGroup)
        {
            ArgumentNullException.ThrowIfNull(muscleGroup, nameof(muscleGroup));
            var entity = muscleGroups.Find(mg => mg.Muscle.ToLower() == muscleGroup.Muscle.ToLower());
            if (entity is not null)
            {
                throw new InvalidOperationException("Muscle group already added");
            }

            muscleGroups.Add(muscleGroup);
        }

        public void RemoveMuscleGroup(Guid muscleGroup)
        {
            ArgumentNullException.ThrowIfNull(muscleGroup, nameof(muscleGroup));

            var entity = muscleGroups.Find(mg => mg.Id == muscleGroup);
            if (entity is null)
            {
                throw new InvalidOperationException("Muscle group not associated with this exercise");
            }
            else
            {
                muscleGroups.Remove(entity);

            }
        }

        public void Update(string name, string description, string descriptionLink, Guid exerciseCategory)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));
            ArgumentException.ThrowIfNullOrEmpty(descriptionLink, nameof(descriptionLink));

            Name = name;
            Description = description;
            DescriptionLink = descriptionLink;
            ExerciseCategory = exerciseCategory;
        }
    }


}
