namespace WorkoutCatalog.Models
{
    public class WorkoutCategory : Entity<Guid>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }


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
