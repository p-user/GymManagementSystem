
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutCatalog.Models;

namespace WorkoutCalalog.Data.Configurations
{
    public class ExcerciseConfigurations : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            throw new NotImplementedException();
        }
    }
}
