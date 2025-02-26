

using Microsoft.EntityFrameworkCore;

namespace StaffManagement.Data
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
        {
        }
       
    }
    
}
