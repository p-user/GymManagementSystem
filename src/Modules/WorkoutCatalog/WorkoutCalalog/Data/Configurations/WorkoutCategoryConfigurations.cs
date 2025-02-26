
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkoutCalalog.Data.Configurations
{
    public class WorkoutCategoryConfigurations : IEntityTypeConfiguration<WorkoutCategory>
    {
        public void Configure(EntityTypeBuilder<WorkoutCategory> builder)
        {
            builder.HasKey(wc => wc.Id);

         
            builder.Property(wc => wc.Name)
                .IsRequired() 
                .HasMaxLength(100); 

            builder.Property(wc => wc.Description)
                .HasMaxLength(500); 

            // Configure the relationship with Workout
            builder.HasMany(wc => wc.Workouts)
                .WithMany(s=>s.WorkoutCategories) // nji workout category ka ma shume se 1 workouts
                .UsingEntity(j => j.ToTable("Workout_WorkoutCategory"));

                
        }
    }
}
