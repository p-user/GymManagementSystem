using Shared.Constants;

namespace WorkoutCalalog.Data
{
    public class WorkoutCatalogDbContext : DbContext
    {
        public WorkoutCatalogDbContext(DbContextOptions<WorkoutCatalogDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutCategory> WorkoutCategories { get; set; }
        public virtual DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemas.WorkoutSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkoutCatalogDbContext).Assembly); //ToDo apply configurations from assembly   

            base.OnModelCreating(modelBuilder);
        }


    }

}
