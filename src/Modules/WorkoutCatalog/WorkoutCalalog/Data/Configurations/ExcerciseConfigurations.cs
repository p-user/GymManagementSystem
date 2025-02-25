
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Models;

namespace WorkoutCalalog.Data.Configurations
{
    public class ExcerciseConfigurations : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id); 

            builder.Property(e => e.Name)
                .IsRequired() 
                .HasMaxLength(100); 

            builder.Property(e => e.Description)
                .HasMaxLength(500); 

            builder.Property(e => e.DescriptionLink)
                .HasMaxLength(200); 

            builder.HasOne<ExerciseCategory>() 
                .WithMany() // Assuming one ExerciseCategory can have many Exercises
                .HasForeignKey(e => e.ExerciseCategory)
                .OnDelete(DeleteBehavior.Cascade); 

            // Configure the relationship with MuscleGroup
            builder.HasMany(e => e.MuscleGroups)
                .WithMany(m=>m.Exercises) // Assuming many-to-many relationship
                .UsingEntity(j => j.ToTable("ExerciseMuscleGroups"));



            // Configure the relationship with Workout
            builder.HasMany(e => e.Workouts)
                .WithMany(s=>s.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseWorkouts"));



            builder.HasIndex(e => e.Name).IsUnique(); // Index on Name
        }
    }
}
