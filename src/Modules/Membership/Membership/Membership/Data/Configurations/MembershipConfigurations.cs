
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Data.Configurations
{
    public class MembershipConfigurations : IEntityTypeConfiguration<Models.Membership>
    {
        public void Configure(EntityTypeBuilder<Models.Membership> builder)
        {
            builder.ToTable("Memberships");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.MembershipStartDate)
            .IsRequired();

            builder.Property(m => m.MembershipEndDate)
                .IsRequired(false);

            builder.Property(m => m.VisitsRemaining)
                .IsRequired();

            builder.Property(m => m.TotalPricePayed)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(m => m.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.HasOne(m => m.MembershipPlan)
           .WithMany(mp => mp.Memberships)
           .HasForeignKey(m => m.MembershipPlanId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.GymMember)
           .WithMany(m => m.Memberships)
           .HasForeignKey(m => m.GymMemberId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.GymMemberId)
           .IsRequired();
        }
    }

}
