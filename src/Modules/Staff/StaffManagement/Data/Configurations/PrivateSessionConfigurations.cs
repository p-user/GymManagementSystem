

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StaffManagement.Data.Configurations
{
    public class PrivateSessionConfigurations : IEntityTypeConfiguration<PrivateSession>
    {
        public void Configure(EntityTypeBuilder<PrivateSession> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.TrainerId)
                .IsRequired();

            builder.Property(s => s.MemberId)
                .IsRequired();

            builder.Property(s => s.ScheduledAt)
                .IsRequired();

            builder.Property(s => s.Status)
                .HasConversion<string>() // Stores enum as string in the DB
                .IsRequired();

            builder.HasIndex(s => new { s.TrainerId, s.ScheduledAt });


            builder.Ignore(s => s.Events);
        }
    }
}
