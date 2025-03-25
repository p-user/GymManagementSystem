

using Shared.Constants;

namespace Membership.Data
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext(DbContextOptions<MembershipDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public virtual DbSet<Models.Membership> Memberships { get; set; }
        public virtual DbSet<Models.MembershipPlan> MembershipPlans { get; set; }
        public virtual DbSet<Models.Member> Members { get; set; }
        public virtual DbSet<Models.Discount> Discounts { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemas.MembershipSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipDbContext).Assembly);
            base.OnModelCreating(modelBuilder);



        }
    }
}
