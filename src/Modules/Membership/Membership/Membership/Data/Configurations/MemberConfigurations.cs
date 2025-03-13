
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Data.Configurations
{
    public class MemberConfigurations : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.FirstName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(m => m.Gender)
                .IsRequired();

            builder.Ignore(m => m.HasActiveMembership);

            builder.HasMany(m => m.Memberships)
           .WithOne(m => m.GymMember)
           .HasForeignKey(m => m.GymMemberId)
           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
