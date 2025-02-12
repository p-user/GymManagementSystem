
namespace WorkoutCatalog.Tests.Workouts.Fixtures
{
    public class MuscleGroupFixture 
    {
        public List<Models.MuscleGroup> MuscleGroups { get; set; }
        public MuscleGroupFixture()
        {
            MuscleGroups = new List<Models.MuscleGroup>
            {
                Models.MuscleGroup.Create("Chest","Chest muscles"),
                Models.MuscleGroup.Create("Shoulders","Shoulder muscles"),
                Models.MuscleGroup.Create("Biceps","Bicep muscles"),
                Models.MuscleGroup.Create("Triceps","Tricep muscles"),
                Models.MuscleGroup.Create("Forearms","Flexor and Extensor muscles"),
                Models.MuscleGroup.Create("Core","Abdominal and Lower back muscles"),
                Models.MuscleGroup.Create("Quadriceps","Quadriceps muscles"),
                Models.MuscleGroup.Create("Glutes","Glute muscles"),
                Models.MuscleGroup.Create("Hamstrings","Hamstrings muscles"),
                Models.MuscleGroup.Create("Calves","Calves muscles")
            };
        }


    }
}
