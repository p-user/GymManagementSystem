

namespace WorkoutCatalog.Tests.Workouts.Fixtures
{
    public class WorkoutFixture
    {

        public List<Models.Workout> Workouts { get; set; }
        public List<Models.Exercise> Exercises { get; set; }

        public List<Models.WorkoutCategory> WorkoutCategories { get; set; }

        public WorkoutFixture()
        {
            Exercises = new ExerciseFixture().Exercises;
            WorkoutCategories = new WorkoutCategoryFixture().WorkoutCategories;

            Workouts = new List<Models.Workout>
            {
                Models.Workout.Create("Strength Training", "Building muscle mass and strength", Exercises, WorkoutCategories.Where(s=>s.Name.Equals("Endurance Training")).ToList()),
                Models.Workout.Create("Endurance Training", "Improving the body's ability to sustain prolonged physical activity", Exercises, WorkoutCategories),
            };
        }
    }
}
