
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Models;

namespace WorkoutCalalog.Data.Configurations
{
    public class ExerciseCategoryConfigurations : IEntityTypeConfiguration<ExerciseCategory>
    {
        public void Configure(EntityTypeBuilder<ExerciseCategory> builder)
        {
            builder.HasKey(ec => ec.Id);

            // Configure properties
            builder.Property(ec => ec.Name)
                .IsRequired() 
                .HasMaxLength(100); 

            builder.Property(ec => ec.Description)
                .HasMaxLength(500); 

            // Configure the relationship with Exercise
            builder.HasMany(ec => ec.Exercises)
                .WithOne() // Assuming one ExerciseCategory can have many Exercises
                .HasForeignKey(e => e.ExerciseCategory)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
