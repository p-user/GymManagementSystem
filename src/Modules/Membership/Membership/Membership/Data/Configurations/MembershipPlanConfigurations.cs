
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Data.Configurations
{
    public class MembershipPlanConfigurations : IEntityTypeConfiguration<MembershipPlan>
    {
        public void Configure(EntityTypeBuilder<MembershipPlan> builder)
        {
            builder.ToTable("MembershipPlans");

            builder.HasKey(mp => mp.Id);

            builder.Property(mp => mp.Name)
           .IsRequired()
           .HasMaxLength(150);

            builder.Property(mp => mp.Price)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(mp => mp.Description)
                .HasMaxLength(500);

            builder.Property(mp => mp.DurationInMonths)
           .IsRequired()
           .HasConversion<int>();

            builder.Property(mp => mp.MaxVisitsPerWeek)
           .IsRequired()
           .HasConversion<int>();

            builder.HasMany(mp => mp.Memberships)
            .WithOne(m => m.MembershipPlan)
            .HasForeignKey(m => m.MembershipPlanId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
