﻿
using WorkoutCatalog.Models;

namespace WorkoutCatalog.Tests.Workouts.Fixtures
{
    public class ExerciseFixture
    {

        public List<Models.Exercise> Exercises { get; set; }
        public ExerciseFixture()
        {

            var categories = new ExerciseCategoryFixture();
            var muscleGroups = new MuscleGroupFixture();

            Exercises = new List<Models.Exercise>
            {
               Exercise.Create( name:"Squat",
               description :"Leg exercise",
               descriptionLink:"https://www.youtube.com/watch?v=tuZ95FlZpxk",
               exerciseCategory : categories.ExerciseCategories.First(c => c.Name == "Compound Exercises").Id,
               muscleGroups : new List<MuscleGroup>
               {
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Quadriceps", StringComparison.OrdinalIgnoreCase)),
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Glutes", StringComparison.OrdinalIgnoreCase)),
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Hamstrings", StringComparison.OrdinalIgnoreCase)),
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Core", StringComparison.OrdinalIgnoreCase))
               }),


              Exercise.Create(
                name: "Bench Press",
                description: "A compound exercise that targets the chest, shoulders, and triceps.",
                descriptionLink: "https://www.youtube.com/watch?v=gl17x1L_8Kc",
                exerciseCategory: categories.ExerciseCategories.First(c => c.Name == "Compound Exercises").Id,
                muscleGroups: new List<MuscleGroup>
                {
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Chest", StringComparison.OrdinalIgnoreCase)),
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Triceps", StringComparison.OrdinalIgnoreCase)),
                    muscleGroups.MuscleGroups.First(mg => mg.Muscle.Equals("Shoulders", StringComparison.OrdinalIgnoreCase))
                }),


              //Exercise.Create(
              //  name: "Pull-Up",
              //  description: "A bodyweight exercise that primarily targets the back and biceps.",
              //  descriptionLink: "https://www.youtube.com/watch?v=cA0OwHOLKm4",
              //  exerciseCategory: categories.ExerciseCategories.First(c => c.Name == "Bodyweight Exercises").Id,
              //  muscleGroups: new List<MuscleGroup>
              //  {
              //      muscleGroups.MuscleGroups.First(mg => mg.Muscle == "Biceps"),
              //      muscleGroups.MuscleGroups.First(mg => mg.Muscle == "Forearms"),
              //      muscleGroups.MuscleGroups.First(mg => mg.Muscle == "Core")
              //  })
              //,

              //Exercise.Create(
              //  name: "Leg Extension",
              //  description: "An isolation exercise that targets the quadriceps muscles.",
              //  descriptionLink: "https://www.youtube.com/watch?v=cA0OwHOLKm4",
              //  exerciseCategory: categories.ExerciseCategories.First(c => c.Name == "Isolation  Exercises").Id,
              //  muscleGroups: new List<MuscleGroup>
              //  {
              //      muscleGroups.MuscleGroups.First(mg => mg.Muscle == "Quadriceps")
              //  })

            };
          

        }
    }
}
