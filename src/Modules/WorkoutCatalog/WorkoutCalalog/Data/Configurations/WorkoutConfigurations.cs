

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Models;

namespace WorkoutCalalog.Data.Configurations
{
    public class WorkoutConfigurations : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasKey(w => w.Id);

            // Configure properties
            builder.Property(w => w.Name)
                .IsRequired() 
                .HasMaxLength(100); 

            builder.Property(w => w.Description)
                .HasMaxLength(500); 

 
            builder.HasMany(w => w.Exercises)
                .WithMany(e => e.Workouts) // Assuming Exercise has a collection of Workouts
                .UsingEntity(j => j.ToTable("WorkoutExercises"));

            builder.HasMany(w => w.WorkoutCategories)
                .WithMany(wc => wc.Workouts) // Assuming WorkoutCategory has a collection of Workouts
                .UsingEntity(j => j.ToTable("WorkoutWorkoutCategories"));
        }
    }
}
