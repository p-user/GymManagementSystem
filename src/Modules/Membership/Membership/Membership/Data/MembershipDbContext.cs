

using Microsoft.EntityFrameworkCore;

namespace Membership.Data
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext(DbContextOptions<MembershipDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Membership");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipDbContext).Assembly);
        }
    }
}
