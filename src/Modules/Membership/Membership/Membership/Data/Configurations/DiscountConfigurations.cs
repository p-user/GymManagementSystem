

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Data.Configurations
{
    public class DiscountConfigurations : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");

            builder.HasKey(d => d.Code);

            builder.Property(d => d.Code)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(d => d.Description)
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.Property(d => d.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasDefaultValue(null);

            builder.Property(d => d.DiscountAmount)
                .HasColumnType("decimal(18, 2)")
                .HasDefaultValue(null);

            builder.Property(d => d.StartDate)
                .HasColumnType("datetime")
                .HasDefaultValue(null);

            builder.Property(d => d.EndDate)
                .HasColumnType("datetime")
                .HasDefaultValue(null);

            builder.Property(d => d.UsageLimit)
                .HasDefaultValue(null);

            builder.Property(d => d.UsageCount)
                .HasDefaultValue(0);

            builder.Property(d => d.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(d => d.AppliesToAllPlans)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(d => d.ApplicablePlans)
                .WithMany(p => p.DiscountsApplicable)
                .UsingEntity(j => j.ToTable("DiscountMembershipPlan")); // Join table for many-to-many 

        }
    }
}
