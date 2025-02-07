

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Models;

namespace WorkoutCalalog.Data.Configurations
{
    public class WorkoutConfigurations : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            throw new NotImplementedException();
        }
    }
}
