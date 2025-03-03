
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffManagement.Models;

namespace StaffManagement.Data.Configurations
{
    public class ReviewConfigurations : IEntityTypeConfiguration<Models.Review>
    {
        public void Configure(EntityTypeBuilder<Models.Review> builder)
        {
            builder.ToTable("TrainerReviews");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasMaxLength(500);

            builder.Property(r => r.ReviewDate)
                   .IsRequired();

          builder.Property(r => r.MemberId)
          .HasConversion(
               id => id.Id,   
               value => new ClientId(value) 
           )
          .HasColumnName("MemberId")
          .IsRequired();
        }
    }
}
