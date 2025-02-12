
namespace WorkoutCatalog.Tests.Workouts.Fixtures
{
    public class WorkoutCategoryFixture 
    {

        public List<Models.WorkoutCategory> WorkoutCategories { get; set; }
        public WorkoutCategoryFixture()
        {

            // Sample test data
            WorkoutCategories = new List<Models.WorkoutCategory>
            {
                Models.WorkoutCategory.Create("Strength Training", "Building muscle mass and strength"),
                Models.WorkoutCategory.Create("Endurance Training", "Improving the body's ability to sustain prolonged physical activity"),
            };

        }
    }
}
