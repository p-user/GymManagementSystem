using WorkoutCatalog.Models;

namespace WorkoutCatalog.Tests.Workouts.Fixtures
{
    public class ExerciseCategoryFixture
    {

        public List<ExerciseCategory> ExerciseCategories { get; set; }
        public ExerciseCategoryFixture()
        {
            ExerciseCategories = new List<ExerciseCategory>
            {
                ExerciseCategory.Create("Compound Exercises", "Engage multiple muscle groups"),
                ExerciseCategory.Create("Bodyweight Exercises", "Use body weight for resistance"),
                ExerciseCategory.Create("Isolation  Exercises", "Target a single muscle group")
            };
        }


    }
}
