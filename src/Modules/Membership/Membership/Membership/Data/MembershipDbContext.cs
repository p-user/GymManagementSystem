

using Microsoft.EntityFrameworkCore;

namespace Membership.Data
{
    public class MembershipDbContext : DbContext
    {
        public MembershipDbContext(DbContextOptions<MembershipDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public virtual  DbSet<Models.GymMember> Members { get; set; }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Membership");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipDbContext).Assembly); //ToDo apply configurations from assembly   


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembershipDbContext).Assembly);
        }
    }
}
