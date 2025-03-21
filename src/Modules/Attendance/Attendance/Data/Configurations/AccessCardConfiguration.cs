

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Data.Configurations
{
    public class AccessCardConfiguration : IEntityTypeConfiguration<AccessCard>
    {

        public void Configure(EntityTypeBuilder<AccessCard> builder)
        {
            builder.HasKey(ac => ac.Id);

            builder.Property(ac => ac.CardNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ac => ac.OwnerType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(ac => ac.IssuedAt)
                .IsRequired();

            builder.HasIndex(ac => ac.CardNumber)
                .IsUnique();

            builder.ToTable("AccessCards");
        }
    }
}
