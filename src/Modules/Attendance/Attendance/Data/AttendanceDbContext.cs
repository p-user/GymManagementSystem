
namespace Attendance.Data
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options)    : base(options)
        {
        }

        public virtual DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public virtual DbSet<AccessCard> AccessCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemas.AttendanceSchema);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AttendanceDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
