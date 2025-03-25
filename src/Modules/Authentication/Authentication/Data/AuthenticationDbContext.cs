using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shared.Constants;

namespace Authentication.Data
{
    public class AuthenticationDbContext : IdentityDbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemas.AuthenticationSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly); //ToDo apply configurations from assembly   

            base.OnModelCreating(modelBuilder);
        }

    }
}
