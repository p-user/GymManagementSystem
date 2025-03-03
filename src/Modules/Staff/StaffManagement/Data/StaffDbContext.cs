

using Microsoft.EntityFrameworkCore;

namespace StaffManagement.Data
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Models.Trainer> Trainers { get; set; }
        public virtual DbSet<Models.Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Staff");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StaffDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
    
}
